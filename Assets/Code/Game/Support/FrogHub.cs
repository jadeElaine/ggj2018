using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHub : MonoBehaviour {

	public float m_screamDelay = 0.75f;
	public float m_screamDistance = 5.0f;
	public float m_screamDuration = 1.0f;
	public float[] m_screamDirections;

	public bool m_canPickUp = true;
	public float m_pickupRange = 1.0f;

	public Transform m_screamBone;
	public Transform m_lowerJawBone;
	public Transform m_upperJawBone;
	public Transform m_leftBrowBone;
	public Transform m_rightBrowBone;
}
