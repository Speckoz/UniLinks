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
		public async Task<DisciplineVO> AddDisciplineTaskAsync(DisciplineVO newDiscipline, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(newDiscipline, token);

			if (response.StatusCode == HttpStatusCode.Created)
				return JsonSerializer.Deserialize<DisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else
				return new DisciplineVO();

			static async Task<IRestResponse> SendRequestTaskAsync(DisciplineVO discipline, string token)
			{
				return await new RequestService()
				{
					Method = Method.POST,
					URL = DataHelper.URLBase,
					URN = "Disciplines/add",
					Body = discipline,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<List<DisciplineVO>>> GetDisciplinesByCoordIdTaskAsync(string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token);

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

			static async Task<IRestResponse> SendRequestTaskAsync(string token)
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

		public async Task<DisciplineVO> UpdateDisciplineTaskAsync(DisciplineVO newDiscipline, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(newDiscipline, token);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<DisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else
				return new DisciplineVO();

			static async Task<IRestResponse> SendRequestTaskAsync(DisciplineVO discipline, string token)
			{
				return await new RequestService()
				{
					Method = Method.PUT,
					URL = DataHelper.URLBase,
					URN = "Disciplines",
					Body = discipline,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<bool> DeleteDisciplineTaskAsync(Guid disciplineId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(disciplineId, token);

			return response.StatusCode == HttpStatusCode.NoContent;

			static async Task<IRestResponse> SendRequestTaskAsync(Guid disciplineId, string token)
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