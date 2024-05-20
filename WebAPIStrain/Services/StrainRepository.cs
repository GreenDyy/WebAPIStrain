using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class StrainRepository : IStrainRepository
    {
        public readonly IrtContext dbContext;
        public static int PAGE_SIZE { get; set; } = 8;
        public int totalPage;
        public StrainRepository(IrtContext context)
        {
            dbContext = context;
        }

        public StrainVM Create(StrainModel strain)
        {
            var _strain = new Strain
            {
                StrainNumber = strain.StrainNumber,
                IdSpecies = strain.IdSpecies,
                IdCondition = strain.IdCondition,
                ImageStrain = strain.ImageStrain,
                ScientificName = strain.ScientificName,
                SynonymStrain = strain.SynonymStrain,
                FormerName = strain.FormerName,
                CommonName = strain.CommonName,
                CellSize = strain.CellSize,
                Organization = strain.Organization,
                Characteristics = strain.Characteristics,
                CollectionSite = strain.CollectionSite,
                Continent = strain.Continent,
                Country = strain.Country,
                IsolationSource = strain.IsolationSource,
                ToxinProducer = strain.ToxinProducer,
                StateOfStrain = strain.StateOfStrain,
                AgitationResistance = strain.AgitationResistance,
                Remarks = strain.Remarks,
                GeneInformation = strain.GeneInformation,
                Publications = strain.Publications,
                RecommendedForTeaching = strain.RecommendedForTeaching,
                DateAdd = strain.DateAdd,
            };
            dbContext.Add(_strain);
            dbContext.SaveChanges();
            //truy vào csdl lấy data mới thêm, dùng VM
            return new StrainVM
            {
                IdStrain = _strain.IdStrain,
                StrainNumber = _strain.StrainNumber,
                IdSpecies = _strain.IdSpecies,
                IdCondition = _strain.IdCondition,
                ImageStrain = _strain.ImageStrain,
                ScientificName = _strain.ScientificName,
                SynonymStrain = _strain.SynonymStrain,
                FormerName = _strain.FormerName,
                CommonName = _strain.CommonName,
                CellSize = _strain.CellSize,
                Organization = _strain.Organization,
                Characteristics = _strain.Characteristics,
                CollectionSite = _strain.CollectionSite,
                Continent = _strain.Continent,
                Country = _strain.Country,
                IsolationSource = _strain.IsolationSource,
                ToxinProducer = _strain.ToxinProducer,
                StateOfStrain = _strain.StateOfStrain,
                AgitationResistance = _strain.AgitationResistance,
                Remarks = _strain.Remarks,
                GeneInformation = _strain.GeneInformation,
                Publications = _strain.Publications,
                RecommendedForTeaching = _strain.RecommendedForTeaching,
                DateAdd = _strain.DateAdd,
                //IdentifyStrains = _strain.IdentifyStrains,
                //Inventories = _strain.Inventories,
                //IsolatorStrains = _strain.IsolatorStrains,
            };
        }
        public bool Delete(int id)
        {
            var strain = dbContext.Strains.FirstOrDefault(s => s.IdStrain == id);
            if (strain != null)
            {
                dbContext.Remove(strain);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<StrainVM> GetAll()
        {
            var strains = dbContext.Strains.Select(strain => new StrainVM
            {
                IdStrain = strain.IdStrain,
                StrainNumber = strain.StrainNumber,
                IdSpecies = strain.IdSpecies,
                IdCondition = strain.IdCondition,
                ImageStrain = strain.ImageStrain,
                ScientificName = strain.ScientificName,
                SynonymStrain = strain.SynonymStrain,
                FormerName = strain.FormerName,
                CommonName = strain.CommonName,
                CellSize = strain.CellSize,
                Organization = strain.Organization,
                Characteristics = strain.Characteristics,
                CollectionSite = strain.CollectionSite,
                Continent = strain.Continent,
                Country = strain.Country,
                IsolationSource = strain.IsolationSource,
                ToxinProducer = strain.ToxinProducer,
                StateOfStrain = strain.StateOfStrain,
                AgitationResistance = strain.AgitationResistance,
                Remarks = strain.Remarks,
                GeneInformation = strain.GeneInformation,
                Publications = strain.Publications,
                RecommendedForTeaching = strain.RecommendedForTeaching,
                DateAdd = strain.DateAdd,
                //IdentifyStrains = strain.IdentifyStrains,
                //Inventories = strain.Inventories,
                //IsolatorStrains = strain.IsolatorStrains
            }).ToList();
            return strains;
        }

        //Lưu ý: thứ tự của bộ lọc là: filtering -> sorting -> paging
        public List<StrainVM> GetAll(string? search, string? sortBy, int page)
        {

            var strains = dbContext.Strains.AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                //có 2 optiop để search, có thể mở rộng thêm
                strains = strains.Where(s => s.ScientificName.Contains(search) || s.StrainNumber.Contains(search));
            }
            #endregion

            #region Sorting
            //sort mặc định là sort theo Scientific_Name
            strains = strains.OrderBy(s => s.IdStrain);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Scientific_Name_Asc":
                        strains = strains.OrderBy(s => s.ScientificName);
                        break;
                    case "Scientific_Name_Desc":
                        strains = strains.OrderByDescending(s => s.ScientificName);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region Paging
            //tính tổng trang sau khi filter và sort chứ chưa paging nha
            totalPage = (int)Math.Ceiling(strains.ToList().Count / (double)PAGE_SIZE);

            strains = strains.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            #endregion
            var result = strains.Select(strain => new StrainVM
            {
                IdStrain = strain.IdStrain,
                StrainNumber = strain.StrainNumber,
                IdSpecies = strain.IdSpecies,
                IdCondition = strain.IdCondition,
                ImageStrain = strain.ImageStrain,
                ScientificName = strain.ScientificName,
                SynonymStrain = strain.SynonymStrain,
                FormerName = strain.FormerName,
                CommonName = strain.CommonName,
                CellSize = strain.CellSize,
                Organization = strain.Organization,
                Characteristics = strain.Characteristics,
                CollectionSite = strain.CollectionSite,
                Continent = strain.Continent,
                Country = strain.Country,
                IsolationSource = strain.IsolationSource,
                ToxinProducer = strain.ToxinProducer,
                StateOfStrain = strain.StateOfStrain,
                AgitationResistance = strain.AgitationResistance,
                Remarks = strain.Remarks,
                GeneInformation = strain.GeneInformation,
                Publications = strain.Publications,
                RecommendedForTeaching = strain.RecommendedForTeaching,
                DateAdd = strain.DateAdd,
                //IdentifyStrains = strain.IdentifyStrains,
                //Inventories = strain.Inventories,
                //IsolatorStrains = strain.IsolatorStrains,
                TotalPage = totalPage,


            }).ToList();
            return result;
        }

        public StrainVM GetById(int id)
        {
            var strain = dbContext.Strains.FirstOrDefault(s => s.IdStrain == id);
            if (strain != null)
            {
                var _strain = new StrainVM
                {
                    IdStrain = strain.IdStrain,
                    StrainNumber = strain.StrainNumber,
                    IdSpecies = strain.IdSpecies,
                    IdCondition = strain.IdCondition,
                    ImageStrain = strain.ImageStrain,
                    ScientificName = strain.ScientificName,
                    SynonymStrain = strain.SynonymStrain,
                    FormerName = strain.FormerName,
                    CommonName = strain.CommonName,
                    CellSize = strain.CellSize,
                    Organization = strain.Organization,
                    Characteristics = strain.Characteristics,
                    CollectionSite = strain.CollectionSite,
                    Continent = strain.Continent,
                    Country = strain.Country,
                    IsolationSource = strain.IsolationSource,
                    ToxinProducer = strain.ToxinProducer,
                    StateOfStrain = strain.StateOfStrain,
                    AgitationResistance = strain.AgitationResistance,
                    Remarks = strain.Remarks,
                    GeneInformation = strain.GeneInformation,
                    Publications = strain.Publications,
                    RecommendedForTeaching = strain.RecommendedForTeaching,
                    DateAdd = strain.DateAdd,
                    //IdentifyStrains = strain.IdentifyStrains,
                    //Inventories = strain.Inventories,
                    //IsolatorStrains = strain.IsolatorStrains
                };
                return _strain;
            }
            return null;
        }

        public StrainVM GetByStrainNumber(string strainNumber)
        {
            var strain = dbContext.Strains.FirstOrDefault(s => s.StrainNumber == strainNumber);
            if (strain != null)
            {
                var _strain = new StrainVM
                {
                    IdStrain = strain.IdStrain,
                    StrainNumber = strain.StrainNumber,
                    IdSpecies = strain.IdSpecies,
                    IdCondition = strain.IdCondition,
                    ImageStrain = strain.ImageStrain,
                    ScientificName = strain.ScientificName,
                    SynonymStrain = strain.SynonymStrain,
                    FormerName = strain.FormerName,
                    CommonName = strain.CommonName,
                    CellSize = strain.CellSize,
                    Organization = strain.Organization,
                    Characteristics = strain.Characteristics,
                    CollectionSite = strain.CollectionSite,
                    Continent = strain.Continent,
                    Country = strain.Country,
                    IsolationSource = strain.IsolationSource,
                    ToxinProducer = strain.ToxinProducer,
                    StateOfStrain = strain.StateOfStrain,
                    AgitationResistance = strain.AgitationResistance,
                    Remarks = strain.Remarks,
                    GeneInformation = strain.GeneInformation,
                    Publications = strain.Publications,
                    RecommendedForTeaching = strain.RecommendedForTeaching,
                    DateAdd = strain.DateAdd,
                    //IdentifyStrains = strain.IdentifyStrains,
                    //Inventories = strain.Inventories,
                    //IsolatorStrains = strain.IsolatorStrains
                };
                return _strain;
            }
            return null;
        }

        public bool Update(int id, StrainModel strain)
        {
            var _strain = dbContext.Strains.FirstOrDefault(s => s.IdStrain == id);
            if (_strain != null)
            {
                _strain.StrainNumber = strain.StrainNumber;
                _strain.IdSpecies = strain.IdSpecies;
                _strain.IdCondition = strain.IdCondition;
                _strain.ImageStrain = strain.ImageStrain;
                _strain.ScientificName = strain.ScientificName;
                _strain.SynonymStrain = strain.SynonymStrain;
                _strain.FormerName = strain.FormerName;
                _strain.CommonName = strain.CommonName;
                _strain.CellSize = strain.CellSize;
                _strain.Organization = strain.Organization;
                _strain.Characteristics = strain.Characteristics;
                _strain.CollectionSite = strain.CollectionSite;
                _strain.Continent = strain.Continent;
                _strain.Country = strain.Country;
                _strain.IsolationSource = strain.IsolationSource;
                _strain.ToxinProducer = strain.ToxinProducer;
                _strain.StateOfStrain = strain.StateOfStrain;
                _strain.AgitationResistance = strain.AgitationResistance;
                _strain.Remarks = strain.Remarks;
                _strain.GeneInformation = strain.GeneInformation;
                _strain.Publications = strain.Publications;
                _strain.RecommendedForTeaching = strain.RecommendedForTeaching;
                _strain.DateAdd = strain.DateAdd;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateStrainNumber(int id, string strainNumber)
        {
            var _strain = dbContext.Strains.FirstOrDefault(s => s.IdStrain == id);
            if (_strain != null)
            {
                _strain.StrainNumber = strainNumber;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
