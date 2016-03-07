using UnityEngine;
using System.Collections;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

[DynamoDBTable("PlayerData_Taxonomy")] //name of the table in amazon
public class PlayerEntity {

    [DynamoDBHashKey] //Hash Key
    public string PlayerID { get; set; }

    [DynamoDBProperty]
    public string LevelID { get; set; }

    //add momre properties as necessary
}
