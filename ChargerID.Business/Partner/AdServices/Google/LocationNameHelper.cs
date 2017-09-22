using System.Collections.Generic;
using ChargerID.Configuration;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201708;
using ChargerID.Business.Models;
using System;
using ChargerID.Business.Exceptions;

namespace ChargerID.Business.Partner.AdServices.Google
{
    public interface ILocationNameHelper
    {
        List<GeoTarget> GetLocationNamesByTargetIds(List<string> ids);
        List<GeoTarget> GetTargetIdsByLocationNames(List<KeyValuePair<string, string>> locationNames);
    }

    public class LocationNameHelper : ILocationNameHelper
    {
        private readonly LocationCriterionService _locationCriterionService;  // Adwords service that provides location names

        public LocationNameHelper(AdWordsUser adwordsUser)
        {
            _locationCriterionService = (LocationCriterionService)adwordsUser.GetService(AdWordsService.v201708.LocationCriterionService);
        }

        public List<GeoTarget> GetLocationNamesByTargetIds(List<string> ids)
        {
            Selector selector = new Selector();
            selector.fields = new string[] { "Id", "LocationName", "CanonicalName", "DisplayType", "ParentLocations", "Reach" };

            // Predicates allow filtering of results
            Predicate IdPredicate = new Predicate();
            IdPredicate.field = "Id";
            IdPredicate.@operator = PredicateOperator.EQUALS;
            IdPredicate.values = ids.ToArray();
            selector.predicates = new Predicate[] { IdPredicate };

            LocationCriterion[] locationCriteria = _locationCriterionService.get(selector);

            List<GeoTarget> targets = new List<GeoTarget>();

            foreach (string id in ids)
            {
                foreach (LocationCriterion target in locationCriteria)
                {
                    if (target.location.id == Convert.ToInt64(id))
                    {
                        targets.Add(new GeoTarget()
                        {
                            Id = id,
                            City = target.location.locationName,
                            State = target.location.parentLocations[0].locationName
                        });
                    }
                }
            }

            return targets;
        }

        public List<GeoTarget> GetTargetIdsByLocationNames(List<KeyValuePair<string, string>> locationNames)
        {
            Selector selector = new Selector();
            selector.fields = new string[] { "Id", "LocationName", "CanonicalName", "DisplayType", "ParentLocations", "Reach" };

            // extract city names from key value pair to use in predicate object
            List<string> cities = new List<string>();
            foreach (KeyValuePair<string, string> location in locationNames)
            {
                cities.Add(location.Key);
            }

            // Predicates allow filtering of results
            Predicate NamePredicate = new Predicate();
            NamePredicate.field = "LocationName";
            NamePredicate.@operator = PredicateOperator.EQUALS;
            NamePredicate.values = cities.ToArray();
            selector.predicates = new Predicate[] { NamePredicate };

            LocationCriterion[] locationCriteria = _locationCriterionService.get(selector);

            List<GeoTarget> targets = new List<GeoTarget>();

            foreach (KeyValuePair<string, string> name in locationNames)
            {
                foreach (LocationCriterion target in locationCriteria)
                {
                    if (target.location.locationName == name.Key && target.location.displayType == "City")
                    {
                        for (int i = 0; i < target.location.parentLocations.Length; i++)
                        {
                            if (target.location.parentLocations[i].displayType == "State" && target.location.parentLocations[i].locationName == name.Value)
                            {
                                targets.Add(new GeoTarget()
                                {
                                    Id = target.location.id.ToString(),
                                    City = name.Key,
                                    State = target.location.parentLocations[i].locationName
                                });
                            }
                        }
                    }
                }
            }

            return targets;
        }
    }
}
