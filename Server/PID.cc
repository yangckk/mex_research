/*
 * Authors:
 * Bill Patterson
 */

#include "PID.h"
#include <google/protobuf/util/time_util.h>

float PID::calcPID()
{
	google::protobuf::util::TimeUtil util;
	m_currTime = util.GetCurrentTime();
	float currError = m_setPoint - m_currState;
	long currMillis = util.TimestampToMilliseconds(m_currTime);
	long prevMillis = util.TimestampToMilliseconds(m_prevTime);
	long dt = currMillis - prevMillis;
	float derivative = (currError - m_prevError) / dt;
	float actuationForce = m_kp * currError + m_kd * derivative + m_ki * m_integral; 
	m_prevError = currError;
	m_prevTime = m_currTime;
	return actuationForce;
}
