using FluentMigrator;

namespace Persistance.Migrations
{
    [Migration(202106280002)]
    public class InitialTables_202106280002 : FluentMigrator.Migration
    {
        public override void Down()
        {
            Delete.Table("Doctor");
            Delete.Table("Patient");
            Delete.Table("Receptionist");
        }

        public override void Up()
        {
            Create.Table("Doctor")
               .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
               .WithColumn("Photo").AsString().NotNullable()
               .WithColumn("FirstName").AsString().NotNullable()
               .WithColumn("LastName").AsString().NotNullable()
               .WithColumn("MiddleName").AsString()
               .WithColumn("DateOfBirth").AsDateTime().NotNullable()
               .WithColumn("Email").AsString().NotNullable()
               .WithColumn("SpecializationId").AsGuid().NotNullable()
               .WithColumn("OfficeId").AsGuid().NotNullable()
               .WithColumn("CareerStartYear").AsDateTime().NotNullable()
               .WithColumn("DoctorStatuses").AsString().NotNullable()
               .WithColumn("AccountId").AsGuid().NotNullable();

            Create.Table("Patient")
               .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
               .WithColumn("Photo").AsString().NotNullable()
               .WithColumn("FirstName").AsString().NotNullable()
               .WithColumn("LastName").AsString().NotNullable()
               .WithColumn("MiddleName").AsString()
               .WithColumn("DateOfBirth").AsDateTime().NotNullable()
               .WithColumn("Email").AsString().NotNullable()
               .WithColumn("PhoneNumber").AsInt32().NotNullable()
               .WithColumn("AccountId").AsGuid().NotNullable();

            Create.Table("Receptionist")
               .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
               .WithColumn("Photo").AsString().NotNullable()
               .WithColumn("FirstName").AsString().NotNullable()
               .WithColumn("LastName").AsString().NotNullable()
               .WithColumn("MiddleName").AsString()
               .WithColumn("Email").AsString().NotNullable()
               .WithColumn("AccountId").AsGuid().NotNullable()
               .WithColumn("OfficeId").AsGuid().NotNullable();
        }
    }
}
