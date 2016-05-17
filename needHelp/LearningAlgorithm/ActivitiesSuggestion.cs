using needHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace needHelp.LearningAlgorithm
{
    public class ActivitiesSuggestion
    {
        #region Members

        private GeneralModel _db = new GeneralModel();
        private int _citiesAmount;
        private int _typesAmount;
        private int _organizationsAmount;
        
        // morning, noon, afternoon, evening , night, early morning
        private const int _dayPartsAmount = 6;
        private const int _daysAmount = 7;
        // TODO: deside the value of the distance threshold... 
        private const int _distanceThreshold = 3;

        private ActivitiesSuggestion _instance;

        #endregion

        #region Singletone

        public ActivitiesSuggestion Instance()
        {
            if (_instance == null)
            {
                _instance = new ActivitiesSuggestion();
            }

            return _instance;
        }

        private ActivitiesSuggestion()
        {
            _citiesAmount = _db.cities.Count();
            _typesAmount = _db.help_types.Count();
            _organizationsAmount = _db.organizations.Count();
        }

        #endregion

        public List<ActivityModels> SuggestActivities(VolunteerModels volunteer)
        {
            // Initialization
            List<ActivityModels> RecommendedActivities = new List<ActivityModels>();
            List<ActivityModels> RegisteredActivities = new List<ActivityModels>();
            List<UserSearchDataModels> UserSearchData = new List<UserSearchDataModels>();
            List<ActivityModels> AllActivities = new List<ActivityModels>();
            List<int[]> RegisteredActivitiesVectors = new List<int[]>();
            List<int[]> NotRegisteredActivitiesVec = new List<int[]>();
            int[] avgVector = new int[_citiesAmount + _typesAmount + _organizationsAmount + _dayPartsAmount + _daysAmount];
            Dictionary<ActivityModels, float> distanceDict = new Dictionary<ActivityModels, float>();

            RegisteredActivities = volunteer.registered_activities.ToList<ActivityModels>();
            UserSearchData = _db.search_data.Where(usr => usr.VolunteerId == volunteer.id).ToList<UserSearchDataModels>();
            AllActivities = _db.activities.ToList<ActivityModels>();

            // Go over all the user's activities and make vectors for them
            int i;
            for (i = 0; i < RegisteredActivities.Count; i++)
            {
                ActivityModels activity = RegisteredActivities.ElementAt(i);
                RegisteredActivitiesVectors[i] = MakeVector(activity.cityId, activity.typeId, activity.organizationId, activity.date);
            }
            
            // Go over all the user's searches and make vectors for them
            for (int j = i + 1; j <  i + UserSearchData.Count + 1; j++)
            {
                UserSearchDataModels usd = UserSearchData.ElementAt(j);
                RegisteredActivitiesVectors[j] = MakeVector(usd.cityId, usd.typeId, usd.organizationId, usd.startDate);
            }

            // Make the average vector from the vectors we have made
            MakeAvgVector(RegisteredActivitiesVectors, avgVector);

            // Go over all the existing activities, if the user isn't registered for them - make a vector for them
            for (int k = 0; k < AllActivities.Count; k++)
            {
                ActivityModels currActivity = AllActivities.ElementAt(k);

                if (!volunteer.registered_activities.Contains(currActivity))
                {
                    NotRegisteredActivitiesVec[k] = MakeVector(currActivity.cityId, currActivity.typeId, currActivity.organizationId, currActivity.date);
                }
            }

            // Calculate the distance between every unregistered activity's vector and the average vector
            CalculateDistance(distanceDict, avgVector, NotRegisteredActivitiesVec);

            // Recommend activities
            for (int l = 0; l < distanceDict.Count; l++)
            {
                if (distanceDict.Values.ElementAt(l) >= _distanceThreshold)
                {
                    RecommendedActivities.Add(distanceDict.Keys.ElementAt(l));
                }
            }

            return RecommendedActivities;
        }

        private int[] MakeVector(int? cityId, int? typeId, int? orgId, DateTime? date)
        {
            //TODO: implement...
            return null;
        }

        private void MakeAvgVector(List<int[]> vectors, int[] avgVector)
        {
            //TODO: implement...
        }

        private void CalculateDistance(Dictionary<ActivityModels, float> dict, int[] avgVector, List<int[]> activitiesVect)
        {
            //TODO: implement...
        }
    }
}