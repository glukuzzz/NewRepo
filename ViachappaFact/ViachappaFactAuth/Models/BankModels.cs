using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViachappaFactAuth.Models.BankModel
{
    public class Opf
    {
        public string type { get; set; }
        public object full { get; set; }
        public object @short { get; set; }
    }

    public class Name
    {
        public string payment { get; set; }
        public object full { get; set; }
        public string @short { get; set; }
    }

    public class State
    {
        public string status { get; set; }
        public long actuality_date { get; set; }
        public long? registration_date { get; set; }
        public object liquidation_date { get; set; }
    }

    public class Metro
    {
        public string name { get; set; }
        public string line { get; set; }
        public double distance { get; set; }
    }

    public class Data2
    {
        public string postal_code { get; set; }
        public string country { get; set; }
        public string country_iso_code { get; set; }
        public string federal_district { get; set; }
        public string region_fias_id { get; set; }
        public string region_kladr_id { get; set; }
        public string region_iso_code { get; set; }
        public string region_with_type { get; set; }
        public string region_type { get; set; }
        public string region_type_full { get; set; }
        public string region { get; set; }
        public object area_fias_id { get; set; }
        public object area_kladr_id { get; set; }
        public object area_with_type { get; set; }
        public object area_type { get; set; }
        public object area_type_full { get; set; }
        public object area { get; set; }
        public string city_fias_id { get; set; }
        public string city_kladr_id { get; set; }
        public string city_with_type { get; set; }
        public string city_type { get; set; }
        public string city_type_full { get; set; }
        public string city { get; set; }
        public string city_area { get; set; }
        public object city_district_fias_id { get; set; }
        public object city_district_kladr_id { get; set; }
        public string city_district_with_type { get; set; }
        public string city_district_type { get; set; }
        public string city_district_type_full { get; set; }
        public string city_district { get; set; }
        public object settlement_fias_id { get; set; }
        public object settlement_kladr_id { get; set; }
        public object settlement_with_type { get; set; }
        public object settlement_type { get; set; }
        public object settlement_type_full { get; set; }
        public object settlement { get; set; }
        public string street_fias_id { get; set; }
        public string street_kladr_id { get; set; }
        public string street_with_type { get; set; }
        public string street_type { get; set; }
        public string street_type_full { get; set; }
        public string street { get; set; }
        public string house_fias_id { get; set; }
        public string house_kladr_id { get; set; }
        public string house_type { get; set; }
        public string house_type_full { get; set; }
        public string house { get; set; }
        public object block_type { get; set; }
        public object block_type_full { get; set; }
        public object block { get; set; }
        public object flat_type { get; set; }
        public object flat_type_full { get; set; }
        public object flat { get; set; }
        public object flat_area { get; set; }
        public object square_meter_price { get; set; }
        public object flat_price { get; set; }
        public object postal_box { get; set; }
        public string fias_id { get; set; }
        public string fias_code { get; set; }
        public string fias_level { get; set; }
        public string fias_actuality_state { get; set; }
        public string kladr_id { get; set; }
        public object geoname_id { get; set; }
        public string capital_marker { get; set; }
        public string okato { get; set; }
        public string oktmo { get; set; }
        public string tax_office { get; set; }
        public string tax_office_legal { get; set; }
        public string timezone { get; set; }
        public string geo_lat { get; set; }
        public string geo_lon { get; set; }
        public string beltway_hit { get; set; }
        public object beltway_distance { get; set; }
        public List<Metro> metro { get; set; }
        public string qc_geo { get; set; }
        public string qc_complete { get; set; }
        public string qc_house { get; set; }
        public object history_values { get; set; }
        public object unparsed_parts { get; set; }
        public string source { get; set; }
        public string qc { get; set; }
    }

    public class Address
    {
        public string value { get; set; }
        public string unrestricted_value { get; set; }
        public Data2 data { get; set; }
    }

    public class Data
    {
        public Opf opf { get; set; }
        public Name name { get; set; }
        public string bic { get; set; }
        public string swift { get; set; }
        public object okpo { get; set; }
        public string correspondent_account { get; set; }
        public string registration_number { get; set; }
        public string payment_city { get; set; }
        public State state { get; set; }
        public object rkc { get; set; }
        public Address address { get; set; }
        public object phones { get; set; }
    }

    public class Suggestion
    {
        public string value { get; set; }
        public string unrestricted_value { get; set; }
        public Data data { get; set; }
    }

    public class RootObject
    {
        public List<Suggestion> suggestions { get; set; }
    }
}