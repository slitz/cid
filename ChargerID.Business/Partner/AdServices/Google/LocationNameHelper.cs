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
        List<KeyValuePair<string, string>> GetLocationNamesByTargetIds(List<string> ids);
        List<KeyValuePair<string, string>> GetTargetIdsByLocationNames(List<string> names);
    }

    public class LocationNameHelper : ILocationNameHelper
    {
        private readonly LocationCriterionService _locationCriterionService;  // Adwords service that provides location names

        public LocationNameHelper(AdWordsUser adwordsUser)
        {
            _locationCriterionService = (LocationCriterionService)adwordsUser.GetService(AdWordsService.v201708.LocationCriterionService);
        }

        public List<KeyValuePair<string, string>> GetLocationNamesByTargetIds(List<string> ids)
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

            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();

            foreach (string id in ids)
            {
                foreach (LocationCriterion location in locationCriteria)
                {
                    if (location.location.id == Convert.ToInt64(id))
                    {
                        pairs.Add(new KeyValuePair<string, string>(id, location.location.locationName));
                    }
                }
            }

            return pairs;
        }

        public List<KeyValuePair<string, string>> GetTargetIdsByLocationNames(List<string> names)
        {
            Selector selector = new Selector();
            selector.fields = new string[] { "Id", "LocationName", "CanonicalName", "DisplayType", "ParentLocations", "Reach" };

            // Predicates allow filtering of results
            Predicate NamePredicate = new Predicate();
            NamePredicate.field = "LocationName";
            NamePredicate.@operator = PredicateOperator.EQUALS;
            NamePredicate.values = names.ToArray();
            selector.predicates = new Predicate[] { NamePredicate };

            LocationCriterion[] locationCriteria = _locationCriterionService.get(selector);

            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();

            foreach (string name in names)
            {
                foreach (LocationCriterion location in locationCriteria)
                {
                    if (location.location.locationName == name)
                    {
                        pairs.Add(new KeyValuePair<string, string>(name, location.location.id.ToString()));
                    }
                }
            }

            return pairs;
        }
    }
}
