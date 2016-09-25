using System.Collections.Generic;
using System.Linq;
using RandomUser.Portable.DTO;
using RandomUser.Portable.ExtensionMethods;
using RandomUser.Portable.Interfaces.Service;
using RandomUser.Portable.Model;

namespace RandomUser.Portable.Service
{
    public class MappingService : IMappingService
    {
        public IEnumerable<User> MapUsers(IEnumerable<Result> dtos)
        {
            var models = from dto in dtos
                         select MapUser(dto);

            return models;
        }

        private User MapUser(Result dto)
        {
            var model = new User();

            if (dto == null)
                return null;

            if (dto.Name != null)
            {
                model.FirstName = dto.Name.First.FirstCharToUpper();
                model.LastName = dto.Name.Last.FirstCharToUpper();
            }

            if (dto.Picture != null)
            {
                model.PictureUrl = dto.Picture.Large;
            }

            model.Gender = dto.Gender;
            model.Cell = dto.Cell;
            model.Phone = dto.Phone;
            model.Email = dto.Email;

            return model;
        }
    }
}