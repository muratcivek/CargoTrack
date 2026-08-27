using CargoTrack.DataAccess.Repositories.Cities;
using CargoTrack.DTO.DTOs.CityDtos;
using CargoTrack.Entity.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.Business.Services.Cities
{
    public class CityService(ICityRepository _cityRepository) : ICityService
    {
        async Task ICityService.CreateAsync(CreateCityDto createCityDto)
        {
            var city = createCityDto.Adapt<City>();
            await _cityRepository.CreateAsync(city);
        }

        async Task ICityService.DeleteAsync(Guid id)
        {
            var city = _cityRepository.GetByIdAsync(id).Result;
            if (city == null)
            {
                throw new ValidationException("City not found");
            }
            await _cityRepository.DeleteAsync(city);
        }

        Task<List<ResultCityDto>> ICityService.GetAllAsync()
        {
            var cities = _cityRepository.GetAllAsync().Result;
            var result = cities.Adapt<List<ResultCityDto>>();
            return Task.FromResult(result);

        }

        Task<ResultCityDto> ICityService.GetByIdAsync(Guid id)
        {
            var city = _cityRepository.GetByIdAsync(id).Result;
            if (city == null)
            {
                throw new ValidationException("City not found");
            }
            return Task.FromResult(city.Adapt<ResultCityDto>());
        }

        async Task ICityService.UpdateAsync(UpdateCityDto updateCityDto)
        {
            var city =  updateCityDto.Adapt<City>();
            await _cityRepository.UpdateAsync(city);
        }
    }
}
