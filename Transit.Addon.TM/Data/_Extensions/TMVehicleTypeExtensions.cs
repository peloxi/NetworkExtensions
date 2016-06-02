﻿using ColossalFramework;
using Transit.Framework.Network;

namespace Transit.Addon.TM.Data
{
    public static class TMVehicleTypeExtensions
    {
        public static ExtendedUnitType ConvertToUnitType(this TMVehicleType vehicleType)
        {
            var result = ExtendedUnitType.None;

            foreach (var vehicleTypes in vehicleType.GetFlags())
            {
                switch (vehicleTypes)
                {
                    case TMVehicleType.Bicycle:
                        result |= ExtendedUnitType.Bicycle;
                        break;
                    case TMVehicleType.Bus:
                        result |= ExtendedUnitType.Bus;
                        break;
                    case TMVehicleType.CargoTrain:
                        result |= ExtendedUnitType.CargoTrain;
                        break;
                    case TMVehicleType.CargoTruck:
                        result |= ExtendedUnitType.CargoTruck;
                        break;
                    case TMVehicleType.Emergency:
                        result |= ExtendedUnitType.Emergency;
                        break;
                    case TMVehicleType.PassengerCar:
                        result |= ExtendedUnitType.PassengerCar;
                        break;
                    case TMVehicleType.PassengerTrain:
                        result |= ExtendedUnitType.PassengerTrain;
                        break;
                    case TMVehicleType.Service:
                        result |= ExtendedUnitType.ServiceVehicle;
                        break;
                    case TMVehicleType.Taxi:
                        result |= ExtendedUnitType.Taxi;
                        break;
                    case TMVehicleType.Tram:
                        result |= ExtendedUnitType.Tram;
                        break;
                }
            }

            return result;
        }
    }
}