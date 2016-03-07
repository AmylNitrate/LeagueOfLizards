using UnityEngine;
using System.Collections;
using Amazon.DynamoDBv2;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;


public class InitializePlayer : MonoBehaviour {
    protected string playerID;

    private string identityPoolID = "ap-northeast-1:d787d691-4a7f-4269-8594-abfb5ba75d2a";  //Needs to be changed depending on app
    public RegionEndpoint EndPoint = RegionEndpoint.APNortheast1;
    public static IAmazonDynamoDB _ddbClient;

    private AWSCredentials _credentials;

    private AWSCredentials Credentials
    {
        get
        {
            if (_credentials == null)
                _credentials = new CognitoAWSCredentials(identityPoolID, EndPoint);
            return _credentials;
        }
    }

    protected IAmazonDynamoDB Client
    {
        get
        {
            if (_ddbClient == null)
            {
                _ddbClient = new AmazonDynamoDBClient(Credentials, EndPoint);

                Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        playerID = Social.localUser.userName;
                        Debug.Log(success);
                    } 
                });
            }

            return _ddbClient;
        }
    }
}
