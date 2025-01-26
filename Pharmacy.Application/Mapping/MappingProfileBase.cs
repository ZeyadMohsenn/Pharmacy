using AutoMapper;
using Pharmacy.Application.Helper;

namespace Pharmacy.Application.Mapping;

public class MappingProfileBase : Profile
{
    protected bool IsArabic => CultureHelper.CurrentLanguage == "ar";

    public MappingProfileBase()
    {
        SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
        DestinationMemberNamingConvention = new PascalCaseNamingConvention();
        ReplaceMemberName("_", "");
    }
}

