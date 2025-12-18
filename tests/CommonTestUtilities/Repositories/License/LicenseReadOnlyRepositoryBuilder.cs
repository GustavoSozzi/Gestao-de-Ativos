using Ativos.Domain.Entities;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Licencas;
using Moq;

namespace CommonTestUtilities.Repositories;

public class LicenseReadOnlyRepositoryBuilder
{
    private readonly Mock<ILicencasReadOnlyRepository> _repository;

    public LicenseReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<ILicencasReadOnlyRepository>();
    }
    
    public LicenseReadOnlyRepositoryBuilder GetById(Licenca licenca)
    {
        _repository.Setup(licenceRepository => licenceRepository.GetById(licenca.Id_Licenca)).ReturnsAsync(licenca);

        return this;
    }

    public ILicencasReadOnlyRepository Build() => _repository.Object;
}