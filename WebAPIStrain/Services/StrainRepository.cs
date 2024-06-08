using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
        //Hoặc lọc theo StrainNumber có ko nha
        public List<StrainVM> GetAll(string? search, string? sortBy, int page)
        {
            var strains = dbContext.Strains
                .Join(dbContext.Inventories,
                      strain => strain.IdStrain,
                      inventory => inventory.IdStrain,
                      (strain, inventory) => new { Strain = strain, Inventory = inventory })
                .AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                // Search strains by ScientificName or StrainNumber
                strains = strains.Where(si => si.Strain.ScientificName.Contains(search) || si.Strain.StrainNumber.Contains(search));
            }
            #endregion

            #region Sorting
            // Default sort by IdStrain
            strains = strains.OrderBy(si => si.Strain.IdStrain);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Scientific_Name_Asc":
                        strains = strains.OrderBy(si => si.Strain.ScientificName);
                        break;
                    case "Scientific_Name_Desc":
                        strains = strains.OrderByDescending(si => si.Strain.ScientificName);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region Paging
            // Calculate total pages after filtering and sorting but before paging
            int totalPage = (int)Math.Ceiling(strains.Count() / (double)PAGE_SIZE);
            strains = strains.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            #endregion

            var result = strains.Select(si => new StrainVM
            {
                IdStrain = si.Strain.IdStrain,
                StrainNumber = si.Strain.StrainNumber,
                IdSpecies = si.Strain.IdSpecies,
                IdCondition = si.Strain.IdCondition,
                ImageStrain = si.Strain.ImageStrain,
                ScientificName = si.Strain.ScientificName,
                SynonymStrain = si.Strain.SynonymStrain,
                FormerName = si.Strain.FormerName,
                CommonName = si.Strain.CommonName,
                CellSize = si.Strain.CellSize,
                Organization = si.Strain.Organization,
                Characteristics = si.Strain.Characteristics,
                CollectionSite = si.Strain.CollectionSite,
                Continent = si.Strain.Continent,
                Country = si.Strain.Country,
                IsolationSource = si.Strain.IsolationSource,
                ToxinProducer = si.Strain.ToxinProducer,
                StateOfStrain = si.Strain.StateOfStrain,
                AgitationResistance = si.Strain.AgitationResistance,
                Remarks = si.Strain.Remarks,
                GeneInformation = si.Strain.GeneInformation,
                Publications = si.Strain.Publications,
                RecommendedForTeaching = si.Strain.RecommendedForTeaching,
                DateAdd = si.Strain.DateAdd,

                TotalPage = totalPage,
                Price = si.Inventory.Price,
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

        public List<StrainVM> GetAllStrainPhylum(int page, string? namePhylum, string? search, string? sortBy)
        {
            var queryStrain = dbContext.Strains.AsQueryable();
            var result = from s in queryStrain
                         join sp in dbContext.Species on s.IdSpecies equals sp.IdSpecies
                         join g in dbContext.Genus on sp.IdGenus equals g.IdGenus
                         join cs in dbContext.Classes on g.IdClass equals cs.IdClass
                         join ph in dbContext.Phylums on cs.IdPhylum equals ph.IdPhylum
                         join sh in dbContext.StrainApprovalHistories on s.IdStrain equals sh.IdStrain
                         join i in dbContext.Inventories on s.IdStrain equals i.IdStrain
                         where ph.NamePhylum.Equals(namePhylum)
                         select new StrainVM
                         {
                             IdStrain = s.IdStrain,
                             StrainNumber = s.StrainNumber,
                             IdSpecies = s.IdSpecies,
                             IdCondition = s.IdCondition,
                             ImageStrain = s.ImageStrain,
                             ScientificName = s.ScientificName,
                             SynonymStrain = s.SynonymStrain,
                             FormerName = s.FormerName,
                             CommonName = s.CommonName,
                             CellSize = s.CellSize,
                             Organization = s.Organization,
                             Characteristics = s.Characteristics,
                             CollectionSite = s.CollectionSite,
                             Continent = s.Continent,
                             Country = s.Country,
                             IsolationSource = s.IsolationSource,
                             ToxinProducer = s.ToxinProducer,
                             StateOfStrain = s.StateOfStrain,
                             AgitationResistance = s.AgitationResistance,
                             Remarks = s.Remarks,
                             GeneInformation = s.GeneInformation,
                             Publications = s.Publications,
                             RecommendedForTeaching = s.RecommendedForTeaching,
                             DateAdd = s.DateAdd,
                             Price = i.Price,
                             TotalPage = totalPage,
                         };
            totalPage = (int)Math.Ceiling(result.ToList().Count / (double)PAGE_SIZE);
            result = result.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                //có 2 optiop để search, có thể mở rộng thêm
                result = result.Where(s => s.ScientificName.Contains(search) || s.StrainNumber.Contains(search));
            }

            #endregion

            #region Sorting
            //sort mặc định là sort theo Scientific_Name
            result = result.OrderBy(s => s.IdStrain);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Scientific_Name_Asc":
                        result = result.OrderBy(s => s.ScientificName);
                        break;
                    case "Scientific_Name_Desc":
                        result = result.OrderByDescending(s => s.ScientificName);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            return result.ToList();
        }

        public List<StrainVM> GetAllStrainClass(int page, string? nameClass, string? search, string? sortBy)
        {
            var queryStrain = dbContext.Strains.AsQueryable();
            var result = from s in queryStrain
                         join sp in dbContext.Species on s.IdSpecies equals sp.IdSpecies
                         join g in dbContext.Genus on sp.IdGenus equals g.IdGenus
                         join cs in dbContext.Classes on g.IdClass equals cs.IdClass
                         join ph in dbContext.Phylums on cs.IdPhylum equals ph.IdPhylum
                         join sh in dbContext.StrainApprovalHistories on s.IdStrain equals sh.IdStrain
                         join i in dbContext.Inventories on s.IdStrain equals i.IdStrain
                         where cs.NameClass.Equals(nameClass)
                         select new StrainVM
                         {
                             IdStrain = s.IdStrain,
                             StrainNumber = s.StrainNumber,
                             IdSpecies = s.IdSpecies,
                             IdCondition = s.IdCondition,
                             ImageStrain = s.ImageStrain,
                             ScientificName = s.ScientificName,
                             SynonymStrain = s.SynonymStrain,
                             FormerName = s.FormerName,
                             CommonName = s.CommonName,
                             CellSize = s.CellSize,
                             Organization = s.Organization,
                             Characteristics = s.Characteristics,
                             CollectionSite = s.CollectionSite,
                             Continent = s.Continent,
                             Country = s.Country,
                             IsolationSource = s.IsolationSource,
                             ToxinProducer = s.ToxinProducer,
                             StateOfStrain = s.StateOfStrain,
                             AgitationResistance = s.AgitationResistance,
                             Remarks = s.Remarks,
                             GeneInformation = s.GeneInformation,
                             Publications = s.Publications,
                             RecommendedForTeaching = s.RecommendedForTeaching,
                             DateAdd = s.DateAdd,
                             Price = i.Price,
                             TotalPage = totalPage,
                         };
            totalPage = (int)Math.Ceiling(result.ToList().Count / (double)PAGE_SIZE);
            result = result.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                //có 2 optiop để search, có thể mở rộng thêm
                result = result.Where(s => s.ScientificName.Contains(search) || s.StrainNumber.Contains(search));
            }

            #endregion

            #region Sorting
            //sort mặc định là sort theo Scientific_Name
            result = result.OrderBy(s => s.IdStrain);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Scientific_Name_Asc":
                        result = result.OrderBy(s => s.ScientificName);
                        break;
                    case "Scientific_Name_Desc":
                        result = result.OrderByDescending(s => s.ScientificName);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            return result.ToList();
        }

        public List<StrainVM> GetAllStrainGenus(int page, string? nameGenus, string? search, string? sortBy)
        {
            var queryStrain = dbContext.Strains.AsQueryable();
            var result = from s in queryStrain
                         join sp in dbContext.Species on s.IdSpecies equals sp.IdSpecies
                         join g in dbContext.Genus on sp.IdGenus equals g.IdGenus
                         join cs in dbContext.Classes on g.IdClass equals cs.IdClass
                         join ph in dbContext.Phylums on cs.IdPhylum equals ph.IdPhylum
                         join sh in dbContext.StrainApprovalHistories on s.IdStrain equals sh.IdStrain
                         join i in dbContext.Inventories on s.IdStrain equals i.IdStrain
                         where g.NameGenus.Equals(nameGenus)
                         select new StrainVM
                         {
                             IdStrain = s.IdStrain,
                             StrainNumber = s.StrainNumber,
                             IdSpecies = s.IdSpecies,
                             IdCondition = s.IdCondition,
                             ImageStrain = s.ImageStrain,
                             ScientificName = s.ScientificName,
                             SynonymStrain = s.SynonymStrain,
                             FormerName = s.FormerName,
                             CommonName = s.CommonName,
                             CellSize = s.CellSize,
                             Organization = s.Organization,
                             Characteristics = s.Characteristics,
                             CollectionSite = s.CollectionSite,
                             Continent = s.Continent,
                             Country = s.Country,
                             IsolationSource = s.IsolationSource,
                             ToxinProducer = s.ToxinProducer,
                             StateOfStrain = s.StateOfStrain,
                             AgitationResistance = s.AgitationResistance,
                             Remarks = s.Remarks,
                             GeneInformation = s.GeneInformation,
                             Publications = s.Publications,
                             RecommendedForTeaching = s.RecommendedForTeaching,
                             DateAdd = s.DateAdd,
                             Price = i.Price,
                             TotalPage = totalPage,
                         };
            totalPage = (int)Math.Ceiling(result.ToList().Count / (double)PAGE_SIZE);
            result = result.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                //có 2 optiop để search, có thể mở rộng thêm
                result = result.Where(s => s.ScientificName.Contains(search) || s.StrainNumber.Contains(search));
            }

            #endregion

            #region Sorting
            //sort mặc định là sort theo Scientific_Name
            result = result.OrderBy(s => s.IdStrain);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Scientific_Name_Asc":
                        result = result.OrderBy(s => s.ScientificName);
                        break;
                    case "Scientific_Name_Desc":
                        result = result.OrderByDescending(s => s.ScientificName);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            return result.ToList();
        }

        public List<StrainVM> GetAllStrainSpecies(int page, string? nameSpecies, string? search, string? sortBy)
        {
            var queryStrain = dbContext.Strains.AsQueryable();
            var result = from s in queryStrain
                         join sp in dbContext.Species on s.IdSpecies equals sp.IdSpecies
                         join g in dbContext.Genus on sp.IdGenus equals g.IdGenus
                         join cs in dbContext.Classes on g.IdClass equals cs.IdClass
                         join ph in dbContext.Phylums on cs.IdPhylum equals ph.IdPhylum
                         join sh in dbContext.StrainApprovalHistories on s.IdStrain equals sh.IdStrain
                         join i in dbContext.Inventories on s.IdStrain equals i.IdStrain
                         where sp.NameSpecies.Equals(nameSpecies)
                         select new StrainVM
                         {
                             IdStrain = s.IdStrain,
                             StrainNumber = s.StrainNumber,
                             IdSpecies = s.IdSpecies,
                             IdCondition = s.IdCondition,
                             ImageStrain = s.ImageStrain,
                             ScientificName = s.ScientificName,
                             SynonymStrain = s.SynonymStrain,
                             FormerName = s.FormerName,
                             CommonName = s.CommonName,
                             CellSize = s.CellSize,
                             Organization = s.Organization,
                             Characteristics = s.Characteristics,
                             CollectionSite = s.CollectionSite,
                             Continent = s.Continent,
                             Country = s.Country,
                             IsolationSource = s.IsolationSource,
                             ToxinProducer = s.ToxinProducer,
                             StateOfStrain = s.StateOfStrain,
                             AgitationResistance = s.AgitationResistance,
                             Remarks = s.Remarks,
                             GeneInformation = s.GeneInformation,
                             Publications = s.Publications,
                             RecommendedForTeaching = s.RecommendedForTeaching,
                             DateAdd = s.DateAdd,
                             Price = i.Price,
                             TotalPage = totalPage,
                         };
            totalPage = (int)Math.Ceiling(result.ToList().Count / (double)PAGE_SIZE);
            result = result.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                //có 2 optiop để search, có thể mở rộng thêm
                result = result.Where(s => s.ScientificName.Contains(search) || s.StrainNumber.Contains(search));
            }

            #endregion

            #region Sorting
            //sort mặc định là sort theo Scientific_Name
            result = result.OrderBy(s => s.IdStrain);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Scientific_Name_Asc":
                        result = result.OrderBy(s => s.ScientificName);
                        break;
                    case "Scientific_Name_Desc":
                        result = result.OrderByDescending(s => s.ScientificName);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            return result.ToList();
        }

        public List<StrainVM> GetRandomStrain()
        {
            var strains = dbContext.Strains
                .OrderBy(x => Guid.NewGuid().ToString())
                .Take(8)
                .Select(strain => new StrainVM
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
                }).ToList();
            return strains;
        }

        public List<StrainVM> GetAllByStraiNumberAndScientificName(string? search)
        {
            var strains = dbContext.Strains.Where(s => s.ScientificName.Contains(search) || s.StrainNumber.Contains(search))
              .Select(strain => new StrainVM
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
              }).ToList();
            return strains;
        }
        public List<StrainVM> GetAllStrainByTheEmployee(string idEmployee)
        {
            var result = from strain in dbContext.Strains
                         join isolatorStrain in dbContext.IsolatorStrains on strain.IdStrain equals isolatorStrain.IdStrain
                         where isolatorStrain.IdEmployee == idEmployee
                         select new StrainVM
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
                             StrainApprovalHistories = strain.StrainApprovalHistories,
                         };

            return result.ToList();
        }
        public bool UpdateImageForStrain(int idStrain, byte[]? img)
        {
            var _strain = dbContext.Strains.FirstOrDefault(s => s.IdStrain == idStrain);
            if (_strain != null)
            {
                _strain.ImageStrain = img;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
