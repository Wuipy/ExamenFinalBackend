using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.WebAPI.Services
{
    public class FraudService : IFraudService
    {
        private readonly LibraryContext _libraryContext;

        public FraudService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<IEnumerable<Fraud>> GetAllAsync()
        {
            return await _libraryContext.Frauds
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<Fraud> CreateAsync(FraudForm dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.ImpostorDetails))
                throw new ArgumentException("ImpostorDetails is required.", "ImpostorDetails");

            if (string.IsNullOrWhiteSpace(dto.ContactInfo))
                throw new ArgumentException("ContactInfo is required.", "ContactInfo");

            if (string.IsNullOrWhiteSpace(dto.Comments))
                throw new ArgumentException("Comments is required.", "Comments");

            var fraud = new Fraud
            {
                ImpostorDetails = dto.ImpostorDetails.Trim(),
                ContactInfo = dto.ContactInfo.Trim(),
                Comments = dto.Comments.Trim(),
                CreatedAt = DateTime.UtcNow,
            };

            await _libraryContext.Frauds.AddAsync(fraud);
            await _libraryContext.SaveChangesAsync();

            return fraud;
        }
    }

    public interface IFraudService
    {
        Task<IEnumerable<Fraud>> GetAllAsync();

        Task<Fraud> CreateAsync(FraudForm dto);
    }
}
