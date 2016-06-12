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
        private const int _distanceThreshold = 2;

        private ActivitiesSuggestion _instance;

        #endregion



        public ActivitiesSuggestion()
        {
            _citiesAmount = _db.cities.Count();
            _typesAmount = _db.help_types.Count();
            _organizationsAmount = _db.organizations.Count();
        }



        public List<ActivityModels> SuggestActivities(VolunteerModels volunteer)
        {
            // Initialization
            List<ActivityModels> RecommendedActivities = new List<ActivityModels>();
            List<ActivityModels> RegisteredActivities = new List<ActivityModels>();
            List<UserSearchDataModels> UserSearchData = new List<UserSearchDataModels>();
            List<ActivityModels> AllActivities = new List<ActivityModels>();
            List<ActivityModels> NotRegisteredActivities = new List<ActivityModels>();
            List<int> UserRequestsActivities = new List<int>();
            List<int[]> RegisteredActivitiesVectors = new List<int[]>();
            List<int[]> NotRegisteredActivitiesVec = new List<int[]>();
            double[] avgVector = new double[_citiesAmount + _typesAmount + _organizationsAmount + _dayPartsAmount + _daysAmount];
            Dictionary<ActivityModels, double> distanceDict = new Dictionary<ActivityModels, double>();

            RegisteredActivities = volunteer.registered_activities.ToList<ActivityModels>();
            UserSearchData = _db.search_data.Where(usr => usr.VolunteerId == volunteer.id).ToList<UserSearchDataModels>();
            UserRequestsActivities = _db.user_requests.Where(u => u.volunteerId == volunteer.id).Select(ur => ur.activityId).ToList<int>();
            AllActivities = _db.activities.ToList<ActivityModels>();

            // Go over all the user's activities and make vectors for them
            foreach (ActivityModels currActivity in RegisteredActivities)
            {
                RegisteredActivitiesVectors.Add(MakeVector(currActivity.cityId, currActivity.typeId, currActivity.organizationId, currActivity.date));
            }
            
            // Go over all the user's searches and make vectors for them
            foreach (UserSearchDataModels usd in UserSearchData)
            {
                RegisteredActivitiesVectors.Add(MakeVector(usd.cityId, usd.typeId, usd.organizationId, usd.startDate));
            }

            // Go over all the user's requests for activities and make vectors for them
            foreach (int currRequest in UserRequestsActivities)
            {
                ActivityModels activity = AllActivities.Where(a => a.id == currRequest).First();
                RegisteredActivitiesVectors.Add(MakeVector(activity.cityId, activity.typeId, activity.organizationId, activity.date));
            }

            // Make the average vector from the vectors we have made
            MakeAvgVector(RegisteredActivitiesVectors, avgVector);

            // Go over all the existing activities, if the user isn't registered for them - make a vector for them
            foreach (ActivityModels currActivity in AllActivities)
            {
                if (!UserRequestsActivities.Contains(currActivity.id))
                {
                    NotRegisteredActivitiesVec.Add(MakeVector(currActivity.cityId, currActivity.typeId, currActivity.organizationId, currActivity.date));
                    NotRegisteredActivities.Add(currActivity);
                }
            }

            // Calculate the distance between every unregistered activity's vector and the average vector
            CalculateDistance(distanceDict, avgVector, NotRegisteredActivitiesVec, NotRegisteredActivities);

            // Recommend activities
            for (int i = 0; i < distanceDict.Count; i++)
            {
                if (distanceDict.Values.ElementAt(i) <= _distanceThreshold)
                {
                    RecommendedActivities.Add(distanceDict.Keys.ElementAt(i));
                }
            }

            if (RecommendedActivities.Count == 0)
            {
                return AllActivities;
            }

            return RecommendedActivities;
        }

        /// <summary>
        /// Make the vector that represent the activity or search-data
        /// </summary>
        /// <param name="cityId">The city id</param>
        /// <param name="typeId">The type id</param>
        /// <param name="orgId">The organization id</param>
        /// <param name="date">The date</param>
        /// <returns></returns>
        private int[] MakeVector(int? cityId, int? typeId, int? orgId, DateTime? date)
        {
            // Initialization
            int[] finalVector = new int[_citiesAmount + _typesAmount + _organizationsAmount + _dayPartsAmount + _daysAmount];
            int[] citiesVec = new int[_citiesAmount];
            int[] typesVec = new int[_typesAmount];
            int[] organizationsVec = new int[_organizationsAmount];
            int[] dayPartsVec = new int[_dayPartsAmount];
            int[] daysVec = new int[_daysAmount];

            try
            {
                // Make cities vector
                if (cityId != null)
                {
                    citiesVec[cityId.Value - 1] = 1;
                }

                // Make types vector
                if (typeId != null)
                {
                    typesVec[typeId.Value - 1] = 1;
                }

                // Make organization vector
                if (orgId != null)
                {
                    organizationsVec[orgId.Value - 1] = 1;
                }
            }
            catch(Exception e)
            {
                // an error accured, most likely because search data wasnt deleted for cities/types that were deleted.
                // TODO : make sure search records are deleted for deleted pro
            }

            if (date != null)
            {
                // Make day-parts vector
                if (date.Value.Hour >= 6 && date.Value.Hour < 12)
                {
                    dayPartsVec[0] = 1;
                }
                else if (date.Value.Hour >= 12 && date.Value.Hour < 16)
                {
                    dayPartsVec[1] = 1;
                }
                else if (date.Value.Hour >= 16 && date.Value.Hour < 18)
                {
                    dayPartsVec[2] = 1;
                }
                else if (date.Value.Hour >= 18 && date.Value.Hour < 21)
                {
                    dayPartsVec[3] = 1;
                }
                else if ((date.Value.Hour >= 21 && date.Value.Hour <= 23) || (date.Value.Hour >= 0 && date.Value.Hour < 3))
                {
                    dayPartsVec[4] = 1;
                }
                else if (date.Value.Hour >= 3 && date.Value.Hour < 6)
                {
                    dayPartsVec[5] = 1;
                }

                // Make days vector
                switch (date.Value.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        {
                            daysVec[0] = 1;
                            break;
                        }
                    case DayOfWeek.Monday:
                        {
                            daysVec[1] = 1;
                            break;
                        }
                    case DayOfWeek.Tuesday:
                        {
                            daysVec[2] = 1;
                            break;
                        }
                    case DayOfWeek.Wednesday:
                        {
                            daysVec[3] = 1;
                            break;
                        }
                    case DayOfWeek.Thursday:
                        {
                            daysVec[4] = 1;
                            break;
                        }
                    case DayOfWeek.Friday:
                        {
                            daysVec[5] = 1;
                            break;
                        }
                    case DayOfWeek.Saturday:
                        {
                            daysVec[6] = 1;
                            break;
                        }
                    default:
                        break;
                }
            }

            // Build the final vector according to all the vectors that were made 
            int i = 0;

            foreach (int city in citiesVec)
            {
                finalVector[i] = city;
                i++;
            }

            foreach (int type in typesVec)
            {
                finalVector[i] = type;
                i++;
            }

            foreach (int organization in organizationsVec)
            {
                finalVector[i] = organization;
                i++;
            }

            foreach (int dayPart in dayPartsVec)
            {
                finalVector[i] = dayPart;
                i++;
            }

            foreach (int day in daysVec)
            {
                finalVector[i] = day;
                i++;
            }

            return finalVector;
        }

        /// <summary>
        /// Make the average vector of all the activities vectors
        /// </summary>
        /// <param name="vectors"></param>
        /// <param name="avgVector"></param>
        private void MakeAvgVector(List<int[]> vectors, double[] avgVector)
        {
            int vectorsAmount = vectors.Count();
            int vectorSize = _citiesAmount + _typesAmount + _organizationsAmount + _dayPartsAmount + _daysAmount;

            // Sum up the values for the i cell in all the vector and divide in the number of vectors
            for (int i = 0; i < vectorSize; i++)
            {
                int sumOfCurrCell = 0;

                for (int j = 0; j < vectorsAmount; j++)
                {
                    sumOfCurrCell += vectors.ElementAt(j)[i];
                }

                avgVector[i] = ((double)sumOfCurrCell / (double)vectorsAmount);
            }
        }

        private void CalculateDistance(Dictionary<ActivityModels, double> dict, double[] avgVector, List<int[]> activitiesVect, List<ActivityModels> activities)
        {
            // Calculate the Euclidean distance between every activity and the average vector 
            for (int i = 0; i < activitiesVect.Count(); i++)
            {
                double sumOfPow = 0;

                for (int j = 0; j < avgVector.Count(); j++)
                {
                    sumOfPow += Math.Pow((avgVector[j] - activitiesVect[i][j]), 2);
                }

                double currDistance = Math.Sqrt(sumOfPow);

                dict.Add(activities.ElementAt(i), currDistance);
            }
        }
    }
}