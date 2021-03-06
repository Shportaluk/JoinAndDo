﻿function SearchUser() {
    var name = $('input[name="name"]').val();
    window.location.href = "/JoinAndDo/search_people?name=" + name + "&gender=" + window.gender + "&status=" + window.status;
}

function OpenUser(id) {
    window.location.href = "/JoinAndDo/peopleId/" + id;
}

function ShowAdvanced() {
    $("#form_search #show").css("z-index", "0");
    $("#form_search #hide").css("z-index", "1");
    $("#form_search #show").animate({ opacity: 0 }, 250);
    $("#form_search #hide").animate({ opacity: 1 }, 250);
    $("#advanced_search").animate({
        height: 125
    }, 700);
}

function HideAdvanced() {
    $("#form_search #show").css("z-index", "1");
    $("#form_search #hide").css("z-index", "0");
    $("#form_search #show").animate({ opacity: 1 }, 250);
    $("#form_search #hide").animate({ opacity: 0 }, 250);
    $("#advanced_search").animate({
        height: 0
    }, 700);
}

$(document).ready(function () {
    window.gender = "All";
    window.status = "All";
    var text = "AALAND ISLANDS|ALA:AFGHANISTAN|AFG:ALBANIA|ALB:ALGERIA|DZA:AMERICAN SAMOA|ASM:ANDORRA|AND:ANGOLA|AGO:ANGUILLA|AIA:ANTARCTICA|ATA:ANTIGUA AND BARBUDA|ATG:ARGENTINA|ARG:ARMENIA|ARM:ARUBA|ABW:AUSTRALIA|AUS:AUSTRIA|AUT:AZERBAIJAN|AZE:BAHAMAS|BHS:BAHRAIN|BHR:BANGLADESH|BGD:BARBADOS|BRB:BELARUS|BLR:BELGIUM|BEL:BELIZE|BLZ:BENIN|BEN:BERMUDA|BMU:BHUTAN|BTN:BOLIVIA|BOL:BOSNIA AND HERZEGOWINA|BIH:BOTSWANA|BWA:BOUVET ISLAND|BVT:BRAZIL|BRA:BRITISH INDIAN OCEAN TERRITORY|IOT:BRUNEI DARUSSALAM|BRN:BULGARIA|BGR:BURKINA FASO|BFA:BURUNDI|BDI:CAMBODIA|KHM:CAMEROON|CMR:CANADA|CAN:CAPE VERDE|CPV:CAYMAN ISLANDS|CYM:CENTRAL AFRICAN REPUBLIC|CAF:CHAD|TCD:CHILE|CHL:CHINA|CHN:CHRISTMAS ISLAND|CXR:COCOS (KEELING) ISLANDS|CCK:COLOMBIA|COL:COMOROS|COM:CONGO, Democratic Republic of (was Zaire)|COD:CONGO, Republic of|COG:COOK ISLANDS|COK:COSTA RICA|CRI:COTE D'IVOIRE|CIV:CROATIA (local n|ame: Hrvatska)                  HRV|   :CUBA|CUB:CYPRUS|CYP:CZECH REPUBLIC|CZE:DENMARK|DNK:DJIBOUTI|DJI:DOMINICA|DMA:DOMINICAN REPUBLIC|DOM:ECUADOR|ECU:EGYPT|EGY:EL SALVADOR|SLV:EQUATORIAL GUINEA|GNQ:ERITREA|ERI:ESTONIA|EST:ETHIOPIA|ETH:FALKLAND ISLANDS (MALVINAS)|FLK:FAROE ISLANDS|FRO:FIJI|FJI:FINLAND|FIN:FRANCE|FRA:FRENCH GUIANA|GUF:FRENCH POLYNESIA|PYF:FRENCH SOUTHERN TERRITORIES|ATF:GABON|GAB:GAMBIA|GMB:GEORGIA|GEO:GERMANY|DEU:GHANA|GHA:GIBRALTAR|GIB:GREECE|GRC:GREENLAND|GRL:GRENADA|GRD:GUADELOUPE|GLP:GUAM|GUM:GUATEMALA|GTM:GUINEA|GIN:GUINEA-BISSAU|GNB:GUYANA|GUY:HAITI|HTI:HEARD AND MC DONALD ISLANDS|HMD:HONDURAS|HND:HONG KONG|HKG:HUNGARY|HUN:ICELAND|ISL:INDIA|IND:INDONESIA|IDN:IRAN (ISLAMIC REPUBLIC OF)|IRN:IRAQ|IRQ:IRELAND|IRL:ISRAEL|ISR:ITALY|ITA:JAMAICA|JAM:JAPAN|JPN:JORDAN|JOR:KAZAKHSTAN|KAZ:KENYA|KEN:KIRIBATI|KIR:KOREA, DEMOCRATIC PEOPLE'S REPUBLIC OF|PRK:KOREA, REPUBLIC OF|KOR:KUWAIT|KWT:KYRGYZSTAN|KGZ:LAO PEOPLE'S DEMOCRATIC REPUBLIC|LAO:LATVIA|LVA:LEBANON|LBN:LESOTHO|LSO:LIBERIA|LBR:LIBYAN ARAB JAMAHIRIYA|LBY:LIECHTENSTEIN|LIE:LITHUANIA|LTU:LUXEMBOURG|LUX:MACAU|MAC:MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF      M|KD :MADAGASCAR|MDG:MALAWI|MWI:MALAYSIA|MYS:MALDIVES|MDV:MALI|MLI:MALTA|MLT:MARSHALL ISLANDS|MHL:MARTINIQUE|MTQ:MAURITANIA|MRT:MAURITIUS|MUS:MAYOTTE|MYT:MEXICO|MEX:MICRONESIA, FEDERATED STATES OF|FSM:MOLDOVA, REPUBLIC OF|MDA:MONACO|MCO:MONGOLIA|MNG:MONTSERRAT|MSR:MOROCCO|MAR:MOZAMBIQUE|MOZ:MYANMAR|MMR:NAMIBIA|NAM:NAURU|NRU:NEPAL|NPL:NETHERLANDS|NLD:NETHERLANDS ANTILLES|ANT:NEW CALEDONIA|NCL:NEW ZEALAND|NZL:NICARAGUA|NIC:NIGER|NER:NIGERIA|NGA:NIUE|NIU:NORFOLK ISLAND|NFK:NORTHERN MARIANA ISLANDS|MNP:NORWAY|NOR:OMAN|OMN:PAKISTAN|PAK:PALAU|PLW:PALESTINIAN TERRITORY, Occupied|PSE:PANAMA|PAN:PAPUA NEW GUINEA|PNG:PARAGUAY|PRY:PERU|PER:PHILIPPINES|PHL:PITCAIRN|PCN:POLAND|POL:PORTUGAL|PRT:PUERTO RICO|PRI:QATAR|QAT:REUNION|REU:ROMANIA|ROU:RUSSIAN FEDERATION|RUS:RWANDA|RWA:SAINT HELENA|SHN:SAINT KITTS AND NEVIS|KNA:SAINT LUCIA|LCA:SAINT PIERRE AND MIQUELON|SPM:SAINT VINCENT AND THE GRENADINES|VCT:SAMOA|WSM:SAN MARINO|SMR:SAO TOME AND PRINCIPE|STP:SAUDI ARABIA|SAU:SENEGAL|SEN:SERBIA AND MONTENEGRO|SCG:SEYCHELLES|SYC:SIERRA LEONE|SLE:SINGAPORE|SGP:SLOVAKIA|SVK:SLOVENIA|SVN:SOLOMON ISLANDS|SLB:SOMALIA|SOM:SOUTH AFRICA|ZAF:SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS|SGS:SPAIN|ESP:SRI LANKA|LKA:SUDAN|SDN:SURINAME|SUR:SVALBARD AND JAN MAYEN ISLANDS|SJM:SWAZILAND|SWZ:SWEDEN|SWE:SWITZERLAND|CHE:SYRIAN ARAB REPUBLIC|SYR:TAIWAN|TWN:TAJIKISTAN|TJK:TANZANIA, UNITED REPUBLIC OF|TZA:THAILAND|THA:TIMOR-LESTE|TLS:TOGO|TGO:TOKELAU|TKL:TONGA|TON:TRINIDAD AND TOBAGO|TTO:TUNISIA|TUN:TURKEY|TUR:TURKMENISTAN|TKM:TURKS AND CAICOS ISLANDS|TCA:TUVALU|TUV:UGANDA|UGA:UKRAINE|UKR:UNITED ARAB EMIRATES|ARE:UNITED KINGDOM|GBR:UNITED STATES|USA:UNITED STATES MINOR OUTLYING ISLANDS|UMI:URUGUAY|URY:UZBEKISTAN|UZB:VANUATU|VUT:VATICAN CITY STATE (HOLY SEE)|VAT:VENEZUELA|VEN:VIET NAM|VNM:VIRGIN ISLANDS (BRITISH)|VGB:VIRGIN ISLANDS (U.S.)|VIR:WALLIS AND FUTUNA ISLANDS|WLF:WESTERN SAHARA|ESH:YEMEN|YEM:ZAMBIA|ZMB:ZIMBABWE|ZWE";
    var separeted = text.split(':');
    for (var i = 0; i < separeted.length; i++) {
        var tmp = separeted[i].split('|');
        var nameCountrie = tmp[0];
        var value = tmp[1];

        $("#list_countries").append('<option value="' + value + '">' + nameCountrie + '</option>');
    }

    $("#select_gender div").click(function () {
        $("#select_gender div").css("backgroundColor", "transparent");
        $("#select_gender div").css("color", "white");
        $(this).css("backgroundColor", "white");
        $(this).css("color", "#2ECCFA");
        window.gender = $(this).text();
    })

    $("#select_status div").click(function () {
        $("#select_status div").css("backgroundColor", "transparent");
        $("#select_status div").css("color", "white");
        $(this).css("backgroundColor", "white");
        $(this).css("color", "#2ECCFA");
        window.status = $(this).text();
    })
});