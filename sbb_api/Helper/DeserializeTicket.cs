using System;
using sbb_api.Model;
using System.Text.RegularExpressions;
using System.Globalization;
using sbb_api.Services;

namespace sbb_api.Helper
{
    public class DeserializeTicket
    {
        private String base64Encoded;

        public DeserializeTicket(String base64Encoded)
        {
            this.base64Encoded = base64Encoded;
        }


        public static Ticket Deserialize(String base64Encoded)
        {
            Ticket ticket = new Ticket();

            var startIndex = base64Encoded.IndexOf("chf", System.StringComparison.CurrentCultureIgnoreCase) ;

            // Get Price of the Ticket
            Regex priceRegex = new Regex("CHF\\s(\\d+\\.\\d+)", RegexOptions.IgnoreCase);
            Match price = priceRegex.Match(base64Encoded);
            if( price.Success )
            {
                ticket.Price = Convert.ToDouble(price.Groups[1].Value);
            } else {
                throw new FormatException("No matching Price found in Mail content.");
            }

            // Get Date of Ticket
            Regex dateRegex = new Regex("Sold:\\r\\n(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.]\\d{4}\\s([01]?\\d|2[0-4]):([0-5]?\\d):([0-5]?\\d)", RegexOptions.IgnoreCase);
            Match date = dateRegex.Match(base64Encoded);
            if( date.Success )
            {
                string dateString = Regex.Replace(date.Groups[0].Value, @"Sold:\s|\n", "");
                DateTimeFormatInfo ukDtfi = new CultureInfo("de-CH", false).DateTimeFormat;

                ticket.CreationDate = Convert.ToDateTime(dateString, ukDtfi);
                Console.WriteLine(ticket);
            } else
            {
                throw new FormatException("No matching creation Date found in Mail content.");
            }

            // Get the Ticket ID
            Regex idRegex = new Regex("\\d{12}", RegexOptions.IgnoreCase);
            Match id = idRegex.Match(base64Encoded);

            if (id.Success)
            {
                ticket._idMailProvider = id.Groups[0].Value;
            }



            ticket.Type = Ticket.TicketType.Regular;
            




            return ticket;

        }


    }
}
