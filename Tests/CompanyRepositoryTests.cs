using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Moq;

namespace Tests;

public class CompanyRepositoryTests
{
    [Fact]
    public void Test1()
    {

    }

    [Fact]
    public void GetAllCompaniesAsync_ReturnsListOfCompanies_WithASingleCompany()
    {
        // Arrange
        var mockRepo = new Mock<ICompanyRepository>(); 
        mockRepo.Setup(repo => repo.GetAllAsync(default)).Returns(ValueTask.FromResult(GetCompanies()));

        // Act
        var result = mockRepo.Object.GetAllAsync(default).GetAwaiter().GetResult().ToList(); 

        // Assert
        Assert.IsType<List<Company>>(result); 
        Assert.Single(result);
    }
    public IEnumerable<Company> GetCompanies()
    {
        return new List<Company> {
            new Company {
                Id = 1,
                Name = "Test Company",
                Active=true,
                Address = new Address
                {
                     Active=true,
                     BoxNumber = "26",
                     City="Las Vegas",
                     CountryId = 22,
                     Street = "Rue Basse",
                     Id = 2,
                     Zipcode = "7170"
                }
            }
        };
    }
}