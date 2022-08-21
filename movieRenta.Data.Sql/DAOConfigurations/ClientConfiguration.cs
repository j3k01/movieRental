using movieRental.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace movieRental.Data.Sql.DAOConfigurations
{
    //Klasa konfiguracyjna encji Category
    //należy zaimplementować (generyczny) interfejs IEntityTypeConfiguration i jako parametr przekazać 
    public class ClientConfiguration : IEntityTypeConfiguration<DAO.Client>
    {
        public void Configure(EntityTypeBuilder<DAO.Client> builder)
        {
            //metoda IsRequired() wymusza w bazie ustawienie koleumny na NotNull
            builder.Property(c => c.ClientId).IsRequired();
            //builder.Property(c => c.ClientFname).IsRequired();
            //builder.Property(c => c.ClientLname).IsRequired();
            //builder.Property(c => c.ClientMail).IsRequired();
            
            //w EF Core domyślnie nazwy tabel są ustawiane w liczbie mnogiej,
            //co jest sprzeczne z dobrymi praktykami nazwania tabel w bazach danych
            //dlatego dzięki metodzie ToTable można ustawić własną nazwę bazy danych
            builder.ToTable("Client");
        }
    }

}
