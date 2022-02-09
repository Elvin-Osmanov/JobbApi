using AutoMapper;
using JobbApi.Api.Client.DTOs;
using JobbApi.Api.Manage.DTOs;
using JobbApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<Company, CompanyGetDto>();
            CreateMap<Company, CompanyItemDto>();

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, Api.Manage.DTOs.CategoryItemDto>();
            CreateMap<Category, Api.Client.DTOs.CategoryItemDto>();
            CreateMap<Category, CategoryGetDto>();

            CreateMap<CityCreateDto, City>();
            CreateMap<City, CityItemDto>();
            CreateMap<City, CityGetDto>();

            CreateMap<CountryCreateDto, Country>();
            CreateMap<Country, CountryItemDto>();
            CreateMap<Country, CountryGetDto>();

            CreateMap<JobCreateDto, Job>();
            CreateMap<Category, CategoryInJobDto>();
            CreateMap<City, CityInJobDto>();
            CreateMap<Country, CountryInJobDto>();
            CreateMap<Company, CompanyInJobDto>();
            CreateMap<Job, JobGetDto>();
            CreateMap<Job, JobItemDto>()
                .ForMember(d => d.CategoryName, f => f.MapFrom(m => m.Category.Name))
                .ForMember(d => d.CompanyName, f => f.MapFrom(m => m.Country.Name))
                .ForMember(d => d.CityName, f => f.MapFrom(m => m.City.Name))
                .ForMember(d => d.CountryName, f => f.MapFrom(m => m.Country.Name));

            CreateMap<Job, JobGetItemDto>()
                .ForMember(d => d.CityName, f => f.MapFrom(m => m.City.Name))
                .ForMember(d => d.CompanyName, f => f.MapFrom(m => m.Company.Name));

            CreateMap<Job, JobFilterDto>();

            CreateMap<Job, JobApplyDto>();

            CreateMap<Job, JobApplyItemDto>()
                .ForMember(d => d.CompanyName, f => f.MapFrom(m => m.Country.Name));
            CreateMap<Job, JobBookmarkItemDto>()
                .ForMember(d => d.CompanyName, f => f.MapFrom(m => m.Country.Name));




            CreateMap<Job, JobBookmarkDto>();
               

            CreateMap<CandidateCreateDto, Candidate>();
            CreateMap<Job, JobInCandidateDto>();
            CreateMap<AppUser, AppUserInCandidateDto>();
            CreateMap<Candidate, CandidateGetDto>();
            CreateMap<Candidate, CandidateItemDto>()
                .ForMember(d => d.AppUserAddress, f => f.MapFrom(m => m.AppUser.Address))
            .ForMember(d => d.AppUserFullName, f => f.MapFrom(m => m.AppUser.FullName))
            .ForMember(d => d.AppUserOccupation, f => f.MapFrom(m => m.AppUser.Occupation))
            .ForMember(d => d.AppUserPhoto, f => f.MapFrom(m => m.AppUser.Photo))
             .ForMember(d => d.JobDeadline, f => f.MapFrom(m => m.Job.Deadline))
              .ForMember(d => d.JobTitle, f => f.MapFrom(m => m.Job.Title));

           

        }
    }
}
