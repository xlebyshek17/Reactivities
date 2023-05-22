using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, 
            int itemsPerPage, int totalItems, int totalPages)
        {
            var pagionationHeader = new 
            {
                currentPage,
                itemsPerPage,
                totalItems,
                totalPages
            };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(pagionationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
 }