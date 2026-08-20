using CargoTrack.DataAccess.Repositories.Abouts;
using CargoTrack.DTO.DTOs.AboutDtos;
using CargoTrack.Entity.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.Business.Services.Abouts
{
    public class AboutService(IAboutRepository _aboutRepository) : IAboutService
    {


        async Task IAboutService.CreateAsync(CreateAboutDto createAboutDto)
        {
            var about = createAboutDto.Adapt<About>();
            
            await _aboutRepository.CreateAsync(about);
        }

        async Task IAboutService.DeleteAsync(Guid id)
        {
             var about = _aboutRepository.GetByIdAsync(id).Result;
             if(about == null)
                throw new ValidationException("About not found");

            await _aboutRepository.DeleteAsync(about);

        }

        async Task<List<ResultAboutDto>> IAboutService.GetAllAsync()
        {
            var abouts = await _aboutRepository.GetAllAsync();
            return abouts.Adapt<List<ResultAboutDto>>();
        }

        async Task<ResultAboutDto> IAboutService.GetByIdAsync(Guid id)
        {
           var about = await _aboutRepository.GetByIdAsync(id);
           if(about == null)
                throw new ValidationException("About not found");
            return about.Adapt<ResultAboutDto>();
        }

        async Task IAboutService.UpdateAsync(UpdateAboutDto updateAboutDto)
        {
           await _aboutRepository.UpdateAsync(updateAboutDto.Adapt<About>());
        }
    }
}
