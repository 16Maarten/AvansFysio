using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DbVektisContext : DbContext
    {
        public DbSet<VektisDiagnosis> Diagnoses { get; set; }

        public DbSet<VektisTreatment> Treatments { get; set; }
        public DbVektisContext(DbContextOptions<DbVektisContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            try
            {
                var readerDiagnoses = new StreamReader(@".\VektisFiles\VektislistDiagnoses.csv", Encoding.Latin1);
                var readerTreatments = new StreamReader(@".\VektisFiles\VektislistTreatments.csv", Encoding.Latin1);

                readerDiagnoses.ReadLine();
                while (!readerDiagnoses.EndOfStream)
                {
                    string[] quotedValues = readerDiagnoses.ReadLine().Split('"');
                    var values = new string[3];
                    if (quotedValues.Length >= 3)
                    {
                        values = quotedValues[0].Split(',');
                        values[2] = quotedValues[1];
                    }
                    else
                    {
                        values = quotedValues[0].Split(',');
                    }

                    if (values[0] != "")
                    {
                        modelBuilder.Entity<VektisDiagnosis>().HasData(new VektisDiagnosis() { Code = Int16.Parse(values[0]), BodyLocation = values[1], Pathology = values[2] });
                    }
                }

                readerTreatments.ReadLine();
                while (!readerTreatments.EndOfStream)
                {
                    string[] quotedValues = readerTreatments.ReadLine().Split('"');
                    var values = new string[3];
                    if (quotedValues.Length >= 3)
                    {
                        values[0] = quotedValues[0].Remove(quotedValues[0].Length - 1);
                        values[1] = quotedValues[1];
                        values[2] = quotedValues[2].Remove(0, 1);
                    }
                    else
                    {
                        values = quotedValues[0].Split(',');
                    }

                    if (values[0] != "")
                    {
                        modelBuilder.Entity<VektisTreatment>().HasData(
                            new VektisTreatment() { Code = values[0], Description = values[1], RemarkRequired = (values[2] == "Ja") });
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Something went wrong with reading out CSV data. Error message: {e}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error message: {e}");
            }

            modelBuilder.Entity<VektisTreatment>()
                .Property("Code")
                .HasMaxLength(10);
        }
    }
}
