﻿using AutoMapper;
using BankingAPI.DTOs.Cliente;
using BankingAPI.DTOs.Cuenta;
using BankingAPI.DTOs.Movimiento;
using BankingAPI.Entities;

namespace BankingAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Cliente, ClienteCreateDTO>().ReverseMap();
            CreateMap<Cliente, ClienteUpdateDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();

            CreateMap<Cuenta, CuentaCreateDTO>().ReverseMap();
            CreateMap<Cuenta, CuentaUpdateDTO>().ReverseMap();
            CreateMap<Cuenta, CuentaDTO>().ReverseMap();

            CreateMap<Movimiento, MovimientoCreateDTO>().ReverseMap();
            CreateMap<Movimiento, MovimientoUpdateDTO>().ReverseMap();
            CreateMap<Movimiento, MovimientoDTO>().ReverseMap();
        }
    }
}
