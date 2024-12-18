using BloomFilter;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace QueryPerformanceTests
{
    [TestFixture]
    public class QueryPerformanceTests
    {
        private readonly string _connectionString;
        private readonly string _storedProcedure;
        private static BloomFilter<string> _bloomFilter;

        public QueryPerformanceTests()
        {
            _connectionString = "Server=MSI;Database=QLCoffee;Trusted_Connection=True;TrustServerCertificate=True;";
            _storedProcedure = "FindEmail";

            // Tạo BloomFilter với kích thước mảng và số lượng hash functions
            // Bạn cần chọn số lượng phần tử dự đoán (capacity) và tỷ lệ sai số (false positive rate)
            _bloomFilter = new BloomFilter<string>(10000, 1);  // Ví dụ: dung lượng 10000 và tỷ lệ sai số 1%

            Console.WriteLine("Constructor called: Connection string, stored procedure, and Bloom Filter initialized.");
        }

        [Test]
        public async Task Test_QueryPerformance_With_BloomFilter_And_1000Calls()
        {
            int totalCalls = 10;
            var totalTimeWithoutBloom = new TimeSpan();
            var totalTimeWithBloom = new TimeSpan();
            var stopwatch = new Stopwatch();

            var emailList = Enumerable.Range(0, 38123123).Select(i => $"test{i}@example.com").ToList();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Giai đoạn 1: Không dùng Bloom Filter
                foreach (var email in emailList)
                {
                    using (var command = new SqlCommand(_storedProcedure, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@email", email);
                        var resultParam = new SqlParameter("@result", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(resultParam);

                        stopwatch.Restart();
                        await command.ExecuteNonQueryAsync();
                        stopwatch.Stop();

                        totalTimeWithoutBloom += stopwatch.Elapsed;
                    }


                }

                var averageTimeWithoutBloom = totalTimeWithoutBloom.TotalMilliseconds / totalCalls;
                var averageTimeWithBloom = totalTimeWithBloom.TotalMilliseconds / totalCalls;

                Console.WriteLine($"Total Calls: {totalCalls}");
                Console.WriteLine($"Total Time Without Bloom Filter: {totalTimeWithoutBloom.TotalMilliseconds} ms");
                Console.WriteLine($"Average Query Time Without Bloom Filter: {averageTimeWithoutBloom} ms");
            }
        }
    }
}
