using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces;

public interface IMerchantPersonRepository
{
    MerchantPerson CreateMerchantPerson(MerchantPerson merchantPerson);

    MerchantPerson? GetMerchantPersonByPersonId(Guid personId);
}
