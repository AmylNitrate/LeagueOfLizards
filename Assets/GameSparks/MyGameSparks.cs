using System;
using System.Collections.Generic;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!

namespace GameSparks.Api.Requests{
	public class LogEventRequest_saveMatchData : GSTypedRequest<LogEventRequest_saveMatchData, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_saveMatchData() : base("LogEventRequest"){
			request.AddString("eventKey", "saveMatchData");
		}
		public LogEventRequest_saveMatchData Set_info( GSData value )
		{
			request.AddObject("info", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_saveMatchData : GSTypedRequest<LogChallengeEventRequest_saveMatchData, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_saveMatchData() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "saveMatchData");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_saveMatchData SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_saveMatchData Set_info( GSData value )
		{
			request.AddObject("info", value);
			return this;
		}
		
	}
	
}
	

namespace GameSparks.Api.Messages {


}
