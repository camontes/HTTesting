namespace HR_Platform.Domain.ValueObjects;

public partial record Address
{
    public Address(string streetAddress, int countryCode, string country, int stateCode, string state, int cityCode, string city, string zipCode)
    {
        StreetAddress = !string.IsNullOrEmpty(streetAddress) ? streetAddress : string.Empty;

        CountryCode = countryCode;
        Country = !string.IsNullOrEmpty(country) ? country : string.Empty;

        StateCode = stateCode;
        State = !string.IsNullOrEmpty(state) ? state : string.Empty;

        CityCode = cityCode;
        City = !string.IsNullOrEmpty(city) ? city : string.Empty;

        ZipCode = !string.IsNullOrEmpty(zipCode) ? zipCode : string.Empty;
    }

    public string StreetAddress { get; init; }
    public int CountryCode { get; init; }
    public string Country { get; init; }
    public int StateCode { get; init; }
    public string State { get; init; }
    public int CityCode { get; init; }
    public string City { get; init; }
    public string ZipCode { get; init; }

    public static Address? Create(string streetAddress, int countryCode, string country, int stateCode, string state, int cityCode, string city, string zipCode)
    {
        //if (string.IsNullOrEmpty(streetAddress) && (countryCode == 0 || string.IsNullOrEmpty(country)))
        //{
        //    return null;
        //}

        return new Address(streetAddress, countryCode, country, stateCode, state, cityCode, city, zipCode);
    }
}
