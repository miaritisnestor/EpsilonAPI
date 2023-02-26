using EpsilonUI.Dtos.Interfaces;
using System.Diagnostics;

namespace EpsilonUI.Dtos
{
    public class CompanyPerson
    {
        public string? printCompanyPersonName(ICompanyPerson companyPerson)
        {
            Debug.WriteLine(companyPerson.Name);
            return companyPerson.Name;
        }



    }

}
