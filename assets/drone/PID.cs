using UnityEngine;
using System.Collections;

public class PID {

	private float pgain;
	private float igain;
	private float dgain;

	private float iState;
	private float last;
	private float WINDUP_GUARD_GAIN = 500 ;

	public PID(float p, float i, float d){
		this.pgain = p;
		this.igain = i;
		this.dgain = d;
	}
	

	
	// get the P gain 
	public float getP(){
		return pgain;
	}
	
	// get the I gain
	public float getI(){
		return igain;
	}
	
	// get the D gain
	public float getD(){
		return dgain;
	}
	
	// set the P gain and store it to eeprom
	public void setP(float p){
		this.pgain = p; 
		//writeFloat(p, pgainAddress);
	}
	
	// set the I gain and store it to eeprom
	public void setI(float i){
		igain = i; 
		//writeFloat(i, igainAddress);
	}
	
	// set the D gain and store it to eeprom
	public void setD(float d){
		dgain = d; 
		//writeFloat(d, dgainAddress);
	}
	
	public float updatePID(float target, float cur, float deltaTime){
		// these local variables can be factored out if memory is an issue, 
		// but they make it more readable
		float error;
		float windupGuard;


		//Use to calculate the correct angle because sometimes the PID try to regulate the difference between 360° and 20° whereas 360° = 0°, because of that the drone loops on the axes Ozx
		/*if (inverse) {
			// Pour angle souhaité 340
			if (cur >= 180) {
				cur = 360 - cur;
			}
		} else {*/
			// Pour angle souhaité 20
			if (cur >= 180) {
				cur = cur - 360;
			}
		//}

		// determine how badly we are doing
		error = target - cur;
		
		// the pTerm is the view from now, the pgain judges 
		// how much we care about error at this instant.
		float pTerm = pgain * error;
		
		// iState keeps changing over time; it's 
		// overall "performance" over time, or accumulated error
		iState += error * deltaTime;
		
		// to prevent the iTerm getting huge despite lots of 
		//  error, we use a "windup guard" 
		// (this happens when the machine is first turned on and
		// it cant help be cold despite its best efforts)
		
		// not necessary, but this makes windup guard values 
		// relative to the current iGain
		windupGuard = WINDUP_GUARD_GAIN / igain;  

		if (iState > windupGuard) 
			iState = windupGuard;
		else if (iState < -windupGuard) 
			iState = -windupGuard;

		float iTerm = igain * iState;
		
		// the dTerm, the difference between the temperature now
		//  and our last reading, indicated the "speed," 
		// how quickly the temp is changing. (aka. Differential)
		float dTerm = (dgain * (cur - last)) / deltaTime;
		
		// now that we've use lastTemp, put the current temp in
		// our pocket until for the next round
		last = cur;
		
		// the magic feedback bit
		return pTerm + iTerm - dTerm;
	}
	
	public void resetError(){
		iState = 0;
	}

}
