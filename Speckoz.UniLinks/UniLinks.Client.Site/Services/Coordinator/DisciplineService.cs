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
	public class DisciplineService
	{
		public async Task<ResultModel<DisciplineVO>> AddDisciplineTaskAsync(DisciplineVO newDiscipline, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.Created => new ResultModel<DisciplineVO>
				{
					Object = JsonSerializer.Deserialize<DisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Disciplina adicionada com sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<DisciplineVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.POST,
					URL = DataHelper.URLBase,
					URN = "Disciplines/add",
					Body = newDiscipline,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<DisciplineVO>> GetDisciplineByDisciplineIdTaskAsync(Guid disciplineId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<DisciplineVO>
				{
					Object = JsonSerializer.Deserialize<DisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<DisciplineVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = $"Disciplines/{disciplineId}",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<List<DisciplineVO>>> GetDisciplinesByCoordIdTaskAsync(string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<List<DisciplineVO>>
				{
					Object = JsonSerializer.Deserialize<List<DisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<List<DisciplineVO>>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = "Disciplines",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<DisciplineVO>> UpdateDisciplineTaskAsync(DisciplineVO newDiscipline, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.Created => new ResultModel<DisciplineVO>
				{
					Object = JsonSerializer.Deserialize<DisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Disciplina atualizada com sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<DisciplineVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.PUT,
					URL = DataHelper.URLBase,
					URN = "Disciplines",
					Body = newDiscipline,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<bool>> DeleteDisciplineTaskAsync(Guid disciplineId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.Created => new ResultModel<bool>
				{
					Object = true,
					Message = "Disciplina removida com sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<bool>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.DELETE,
					URL = DataHelper.URLBase,
					URN = $"Disciplines/{disciplineId}",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}
	}
}