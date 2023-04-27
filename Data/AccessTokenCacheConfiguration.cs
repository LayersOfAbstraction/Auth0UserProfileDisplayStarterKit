using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class AccessTokenCacheConfiguration : IEntityTypeConfiguration<AccessTokenCache>  
{    
    public void Configure(EntityTypeBuilder<AccessTokenCache> builder)  
    {  
        builder.ToTable(name: "tblAccessTokenCache", schema: "security");  
  
        builder.HasIndex(e => e.ExpiresAtTime);  
  
        builder.Property(e => e.Id)  
            .IsRequired()  
            .HasMaxLength(449);  
  
        builder.HasKey(e => e.Id);  
  
        builder.Property(e => e.Value).IsRequired();  
    }  
} 