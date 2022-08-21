using movieRental.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace movieRental.Data.Sql.DAOConfigurations
{
    //Klasa konfiguracyjna encji Category
    //należy zaimplementować (generyczny) interfejs IEntityTypeConfiguration i jako parametr przekazać 
    public class MovieOrderConfiguration : IEntityTypeConfiguration<MovieOrder>
    {
        public void Configure(EntityTypeBuilder<MovieOrder> builder)
        {
            //metoda IsRequired() wymusza w bazie ustawienie koleumny na NotNull
            builder.Property(c => c.MovieOrderId).IsRequired();
            builder.Property(c => c.MovieId).IsRequired();
            builder.Property(c => c.OrderId).IsRequired();
            builder.HasOne(x => x.Movie)
                .WithMany(x => x.MovieOrders)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.MovieId);
            builder.HasOne(x => x.Order)
                .WithMany(x => x.MovieOrders)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.OrderId);
            //w EF Core domyślnie nazwy tabel są ustawiane w liczbie mnogiej,
            //co jest sprzeczne z dobrymi praktykami nazwania tabel w bazach danych
            //dlatego dzięki metodzie ToTable można ustawić własną nazwę bazy danych
            builder.ToTable("MovieOrder");
        }
    }

}
