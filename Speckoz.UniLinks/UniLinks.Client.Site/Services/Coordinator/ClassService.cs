using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Helper;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Services.Coordinator
{
    public class ClassService
    {
        public async Task<ResultModel<ClassVO>> AddClasseTaskAsync(ClassVO newClass, string token)
        {
            IRestResponse resp = await SendRequestTaskAsync();

            return resp.StatusCode switch
            {
                HttpStatusCode.Created => new ResultModel<ClassVO>
                {
                    Object = JsonSerializer.Deserialize<ClassVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                    Message = "Sala Sucesso!",
                    StatusCode = resp.StatusCode
                },

                _ => new ResultModel<ClassVO>
                {
                    Message = resp.Content.Replace("\"", string.Empty),
                    StatusCode = resp.StatusCode
                }
            };

            async Task<IRestResponse> SendRequestTaskAsync()
            {
                return await new RequestService()
                {
                    Method = Method.POST,
                    URL = DataHelper.URLBase,
                    URN = "Classes",
                    Body = newClass,
                    Authenticator = new JwtAuthenticator(token)
                }.ExecuteTaskAsync();
            }
        }

        public async Task<ResultModel<List<ClassVO>>> GetClassesTaskAsync(string token)
        {
            IRestResponse resp = await SendRequestTaskAsync(token);

            return resp.StatusCode switch
            {
                HttpStatusCode.OK => new ResultModel<List<ClassVO>>
                {
                    Object = JsonSerializer.Deserialize<List<ClassVO>>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                    Message = "Sucesso!",
                    StatusCode = resp.StatusCode
                },

                _ => new ResultModel<List<ClassVO>>
                {
                    Message = resp.Content.Replace("\"", string.Empty),
                    StatusCode = resp.StatusCode
                }
            };

            async Task<IRestResponse> SendRequestTaskAsync(string token)
            {
                return await new RequestService()
                {
                    Method = Method.GET,
                    URL = DataHelper.URLBase,
                    URN = "Classes/all",
                    Authenticator = new JwtAuthenticator(token)
                }.ExecuteTaskAsync();
            }
        }

        public async Task<ResultModel<List<ClassVO>>> GetClassesByCourseIdAndPeriodTaskAsync(Guid courseId, int period, string token)
        {
            IRestResponse resp = await SendRequestTaskAsync();


            return resp.StatusCode switch
            {
                HttpStatusCode.OK => new ResultModel<List<ClassVO>>
                {
                    Object = JsonSerializer.Deserialize<List<ClassVO>>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                    Message = "Sucesso!",
                    StatusCode = resp.StatusCode
                },

                _ => new ResultModel<List<ClassVO>>
                {
                    Message = resp.Content.Replace("\"", string.Empty),
                    StatusCode = resp.StatusCode
                }
            };

            async Task<IRestResponse> SendRequestTaskAsync()
            {
                var request = new RequestService()
                {
                    Method = Method.GET,
                    URL = DataHelper.URLBase,
                    URN = $"Classes/{courseId}",
                    Authenticator = new JwtAuthenticator(token)
                };
                request.Parameters.Add("period", period.ToString());
                return await request.ExecuteTaskAsync();
            }
        }

        public async Task<ResultModel<ClassVO>> UpdateClassTaskAsync(ClassVO newClass, string token)
        {
            IRestResponse resp = await SendRequestTaskAsync();

            return resp.StatusCode switch
            {
                HttpStatusCode.OK => new ResultModel<ClassVO>
                {
                    Object = JsonSerializer.Deserialize<ClassVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                    Message = "Sala atualizada com sucesso!",
                    StatusCode = resp.StatusCode
                },

                _ => new ResultModel<ClassVO>
                {
                    Message = resp.Content.Replace("\"", string.Empty),
                    StatusCode = resp.StatusCode
                }
            };

            async Task<IRestResponse> SendRequestTaskAsync()
            {
                return await new RequestService()
                {
                    Method = Method.PUT,
                    URL = DataHelper.URLBase,
                    URN = "Classes",
                    Body = newClass,
                    Authenticator = new JwtAuthenticator(token)
                }.ExecuteTaskAsync();
            }
        }

        public async Task<ResultModel<bool>> RemoveClassTaskAsync(Guid classId, string token)
        {
            IRestResponse resp = await SendRequestTaskAsync();

            return resp.StatusCode switch
            {
                HttpStatusCode.NoContent => new ResultModel<bool>
                {
                    Object = true,
                    Message = "Sala removida com sucesso",
                    StatusCode = resp.StatusCode
                },

                _ => new ResultModel<bool>
                {
                    Message = resp.Content.Replace("\"", string.Empty),
                    StatusCode = resp.StatusCode
                }
            };

            async Task<IRestResponse> SendRequestTaskAsync()
            {
                return await new RequestService()
                {
                    Method = Method.DELETE,
                    URL = DataHelper.URLBase,
                    URN = $"Classes/{classId}",
                    Authenticator = new JwtAuthenticator(token)
                }.ExecuteTaskAsync();
            }
        }

        public async Task<ResultModel<ClassVO>> GetClassTaskAsync(Guid classId, string token)
        {
            IRestResponse resp = await SendRequestTaskAsync();

            return resp.StatusCode switch
            {
                HttpStatusCode.OK => new ResultModel<ClassVO>
                {
                    Object = JsonSerializer.Deserialize<ClassVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                    Message = "Sucesso",
                    StatusCode = resp.StatusCode
                },

                _ => new ResultModel<ClassVO>
                {
                    Message = resp.Content.Replace("\"", string.Empty),
                    StatusCode = resp.StatusCode
                }
            };

            async Task<IRestResponse> SendRequestTaskAsync()
            {
                return await new RequestService()
                {
                    Method = Method.GET,
                    URL = DataHelper.URLBase,
                    URN = $"Classes/{classId}",
                    Authenticator = new JwtAuthenticator(token)
                }.ExecuteTaskAsync();
            }
        }
    }
}