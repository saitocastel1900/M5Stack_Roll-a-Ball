#include <M5Stack.h>
#include "utility/MPU9250.h"

MPU9250 IMU;

void readGyro(float *gy){
  gy[0] = 0.0;
  gy[1] = 0.0;
  gy[2] = 0.0;
  if (IMU.readByte(MPU9250_ADDRESS, INT_STATUS) & 0x01) {
    IMU.readGyroData(IMU.gyroCount);  // ジャイロの生データーを取得する
    IMU.getGres();  // スケール値を取得する
    // x/y/z軸のジャイロ値を計算する
    gy[0] = IMU.gx = (float)IMU.gyroCount[0] * IMU.gRes;
    gy[1] = IMU.gy = (float)IMU.gyroCount[1] * IMU.gRes;
    gy[2] = IMU.gz = (float)IMU.gyroCount[2] * IMU.gRes;
  }
}

void setup() {
  M5.begin();
  
  Serial.begin(115200);
  Wire.begin();

  IMU.initMPU9250();
  IMU.initAK8963(IMU.magCalibration);

  M5.Lcd.setTextSize(2);
  M5.Lcd.setCursor(0,0);
  M5.Lcd.println("MPU9250 / AK8963");
}

void loop() {
  float gy[3];
  readGyro(gy);

  Serial.printf("%5.2f,%5.2f,%5.2f\n",gy[0],gy[1],gy[2]);
  Serial.println();

  delay(10);
}
