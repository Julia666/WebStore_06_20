using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args) =>  CreateHostBuilder(args).Build().Run(); 
        // Run() - ��������� ���� �� ������������� (����������� ���� � ������� ����� � ���� �������� ������ �������� �����������)

        // �������� �������-����������� �����
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host =>host
                    .UseStartup<Startup>()
                    //.UseUrls("http://localhost:5000") // ����� ���, ����� ����� ���������
                                                        // ��������� ���� ���������� ����, ����� ������ ������ ������� ����� ������������
                                                        // ���� �� ���������� - �� ����� ������� ����� � ����������, ��������, � ��������
                );
    }
}
 