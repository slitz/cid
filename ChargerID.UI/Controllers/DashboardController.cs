﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using ChargerID.UI.Models;
using ChargerID.DataAccess;
using ChargerID.Configuration;

namespace ChargerID.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDataAccess _data;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public DashboardController()
            : this(null, null)
        {

        }

        public DashboardController(IDataAccess data = null, IConfig config = null)
        {
            _data = data ?? new DataAccess.DataAccess();
            _config = config ?? new Config();
        }

        // GET /Map
        [AllowAnonymous]
        public ActionResult Index()
        {
            var mapModel = new MapModel();
            mapModel.MapDataList = GetMapData();
            return View(mapModel);
        }

        /// <summary>
        /// Retrieve's the latest location data from the database for plotting on a map
        /// </summary>
        /// <returns></returns>
        private List<MapData> GetMapData()
        {
            List<MapData> list = new List<MapData>();
            foreach (metropolitan_area metro in _data.GetAllMetropolitanAreas())
            {
                location location = _data.GetLocationsByMetropolitanAreaId(metro.id).FirstOrDefault();
                charging_station_data chargingStationData = _data.GetChargingStationDataByPostalCode(location.postal_code).LastOrDefault();
                list.Add(new MapData() { 
                    Latitude = location.latitude.ToString(), 
                    Longitude = location.longitude.ToString(), 
                    Stations = Convert.ToInt32(chargingStationData.station_count),
                    Ports = Convert.ToInt32(chargingStationData.port_count),
                    City = location.city,
                    State = location.state
                });
            };

            return list;
        }
    }
}