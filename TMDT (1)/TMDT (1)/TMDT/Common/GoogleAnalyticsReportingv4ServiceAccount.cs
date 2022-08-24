using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;
using System.Collections.Generic;

namespace Common
{
    public class GoogleApiGetData
    {
        private GetReportsResponse getReportsGA(string Json, string viewid /* id of the view you want to read from*/)
        {
            // create reportingService
            string[] scopes = { AnalyticsReportingService.Scope.AnalyticsReadonly };
            AnalyticsReportingService reportingService = new AnalyticsReportingService(
                new BaseClientService.Initializer
                {
                    HttpClientInitializer =  GoogleCredential.FromJson(Json).CreateScoped(scopes)
                });

            // getReportsRequest data for service
            DateRange dateRange = new DateRange
            {
                StartDate = "2019-01-01",
                EndDate = DateTime.Now.ToString("yyyy-MM-dd")
            };
            Dimension date = new Dimension { Name = "ga:date" };
            Metric sessions = new Metric
            {
                Expression = "ga:pageviews",
                Alias = "Sessions"
            };
            ReportRequest reportRequest = new ReportRequest
            {
                DateRanges = new List<DateRange> { dateRange },
                Dimensions = new List<Dimension> { date },
                Metrics = new List<Metric> { sessions },
                ViewId = viewid
            };
            GetReportsRequest getReportsRequest = new GetReportsRequest
            {
                ReportRequests = new List<ReportRequest> { reportRequest }
            };

            // Execute to get response Reports
            ReportsResource.BatchGetRequest batchRequest = reportingService.Reports.BatchGet(getReportsRequest);
            GetReportsResponse response = batchRequest.Execute();
            return response;
        }

        public int Pageview(string Json, string viewid)
        {
            GetReportsResponse response = getReportsGA(Json, viewid);
            // return Metrics
            int total = 0;
            foreach (ReportRow x in response.Reports[0].Data.Rows)
            {
                total += int.Parse(string.Join(", ", x.Metrics[0].Values));
            }
            return total;
        }

        public List<Graph> Graphs(string Json, string viewid)
        {
            GetReportsResponse response = getReportsGA(Json, viewid);
            // return Metrics Graph
            List<Graph> Temp_Graph = new List<Graph>();
            foreach (ReportRow x in response.Reports[0].Data.Rows)
            {
                string Temp = string.Join(", ", x.Dimensions);
                string Temp_Date = Temp.Substring(0, 4) + "-" + Temp.Substring(4, 2) + "-" + Temp.Substring(6, 2);
                Temp_Graph.Add(new Graph { Date = Temp_Date, Views = string.Join(", ", x.Metrics[0].Values) });
            }
            return Temp_Graph;
        }
    }

    public class Graph
    {
        public string Date;
        public string Views;
    }
}