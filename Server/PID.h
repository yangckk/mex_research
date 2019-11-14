/*
 * Authors:
 * Bill Patterson
 */

#include <google/protobuf/util/time_util.h>


class PID
{
	public:
		PID() 
		{
			google::protobuf::util::TimeUtil util;
			m_prevTime = util.GetCurrentTime();
			m_currTime = util.GetCurrentTime();
			m_prevError = 0.0;
			m_integral = 0.0;
			m_kp = 0.0;
			m_kd = 0.0;
			m_ki = 0.0;
			m_setPoint = 0.0;
			m_currState = 0.0;
		}
		
		PID(float kp, float kd, float ki, float sp, int id)
			:m_kp(kp), m_kd(kd), m_ki(ki), m_id(id), m_setPoint(sp) 
		{
			google::protobuf::util::TimeUtil util;
			m_prevTime = util.GetCurrentTime();
			m_currTime = util.GetCurrentTime();
			m_prevError = 0.0;
			m_integral = 0.0;
		}
		
		float calcPID();
		
		void setCurrState(float state)
		{
			m_currState = state;
		}

		void setGains(float kp, float kd, float ki) { m_kp = kp; m_kd = kd; m_ki = ki; }

		void setID(int id) { m_id = id; }

		void setSetPoint(float sp) { m_setPoint = sp; }

		int getID() { return m_id; }

	private:
		float m_kp;
		float m_kd;
		float m_ki;
		int m_id;
		float m_currState;
		float m_prevError;
		float m_integral;
		float m_setPoint;
		google::protobuf::Timestamp m_prevTime;
		google::protobuf::Timestamp m_currTime;
};
