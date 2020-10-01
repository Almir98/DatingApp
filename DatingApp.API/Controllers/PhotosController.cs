using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Helpers;
using DatingApp.API.Interface;
using DatingApp.API.ViewModels;
using DatingApp.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        protected readonly IDating _repository;
        protected readonly IMapper _mapper;
        protected readonly IOptions<CloudinarySettings> _cloudConfig;
        protected readonly Cloudinary _cloudinary;


        public PhotosController(IDating repo,IMapper mapper, IOptions<CloudinarySettings> cloudConfig)
        {
            _repository = repo;
            _mapper = mapper;
            _cloudConfig = cloudConfig;

            Account acc = new Account
            (
               _cloudConfig.Value.CloudName,
               _cloudConfig.Value.ApiKey,
               _cloudConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId,PhotoForCreationVM photoCreation)
        {
            if (userId == 0)
                return Unauthorized();

            var userRepo = await _repository.GetUser(userId);
            var file = photoCreation.File;
            var uploadResult = new ImageUploadResult();

            if(file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation=new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoCreation.Url = uploadResult.Url.ToString();
            photoCreation.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoCreation);
            if(!userRepo.Photos.Any(e => e.IsMain))
                photo.IsMain=true;

            userRepo.Photos.Add(photo);

            if(await _repository.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Could not add a photo");
        }


















    }
}