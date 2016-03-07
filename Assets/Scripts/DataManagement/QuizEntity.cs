using UnityEngine;
using System.Collections;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

[DynamoDBTable("QuizData_Taxonomy")]    //name of the table in amazon
public class QuizEntity {

    [DynamoDBHashKey] //Hash Key
    public string PlayerID { get; set; }

    [DynamoDBProperty]
    public string Q1Answer { get; set; }

    //Add more properties as necessary
}
