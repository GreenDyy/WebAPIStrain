using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly IrtContext dbContext;

        public ConditionRepository(IrtContext context)
        {
            dbContext = context;
        }

        public ConditionalStrainVM Create(ConditionalStrainModel inputCondition)
        {
            var newCondition = new ConditionalStrain
            {
                Medium = inputCondition.Medium,
                Temperature = inputCondition.Temperature,
                LightIntensity = inputCondition.LightIntensity,
                Duration = inputCondition.Duration,
            };
            dbContext.Add(newCondition);
            dbContext.SaveChanges();
            return new ConditionalStrainVM
            {
                IdCondition = newCondition.IdCondition,
                Medium = newCondition.Medium,
                Temperature = newCondition.Temperature,
                LightIntensity = newCondition.LightIntensity,
                Duration = newCondition.Duration,
            };
        }

        public bool Delete(int id)
        {
            var condition = dbContext.ConditionalStrains.FirstOrDefault(p => p.IdCondition == id);
            if (condition != null)
            {
                dbContext.Remove(condition);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ConditionalStrainVM> GetAll()
        {
            var conditions = dbContext.ConditionalStrains.Select(p => new ConditionalStrainVM
            {
                IdCondition = p.IdCondition,
                Medium = p.Medium,
                Temperature = p.Temperature,
                LightIntensity = p.LightIntensity,
                Duration = p.Duration,
            }).ToList();
            return conditions;
        }

        public ConditionalStrainVM GetById(int id)
        {
            var condition = dbContext.ConditionalStrains.FirstOrDefault(p => p.IdCondition == id);
            if (condition != null)
            {
                return new ConditionalStrainVM
                {
                    IdCondition = condition.IdCondition,
                    Medium = condition.Medium,
                    Temperature = condition.Temperature,
                    LightIntensity = condition.LightIntensity,
                    Duration = condition.Duration,
                };
            }
            return null;
        }

        public bool Update(int id, ConditionalStrainModel inputCondition)
        {
            var _condition = dbContext.ConditionalStrains.FirstOrDefault(p => p.IdCondition == id);
            if (_condition != null)
            {
                _condition.Medium = inputCondition.Medium;
                _condition.Temperature = inputCondition.Temperature;
                _condition.LightIntensity = inputCondition.LightIntensity;
                _condition.Duration = inputCondition.Duration;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
