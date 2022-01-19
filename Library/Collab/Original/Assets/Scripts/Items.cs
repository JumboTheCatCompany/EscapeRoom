using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
     public string Name { get; set; }
     public string Description { get; set; }
     public List<Items> MixedWith { get; set; }
}
