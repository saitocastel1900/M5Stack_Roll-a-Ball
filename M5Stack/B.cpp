#include <M5Stack.h>
#include "utility/MPU9250.h"

MPU9250 IMU;

#define NUM_SAMPLES 3 // 移動平均に使用するサンプル数

float prevAcc[NUM_SAMPLES][3]; // 直近のサンプルデータを保存
int sampleIndex = 0;

void readAcc(float *ac) {
  ac[0] = 0.0;
  ac[1] = 0.0;
  ac[2] = 0.0;
  if (IMU.readByte(MPU9250_ADDRESS, INT_STATUS) & 0x01) {
    IMU.readAccelData(IMU.accelCount);
    IMU.getAres();
    ac[0] = (float)IMU.accelCount[0] * IMU.aRes;
    ac[1] = (float)IMU.accelCount[1] * IMU.aRes;
    ac[2] = (float)IMU.accelCount[2] * IMU.aRes;
  }
}

// 移動平均フィルターを適用して加速度データを平滑化
void applyMovingAverage(float *ac) {
  for (int i = 0; i < 3; i++) {
    // 直近のNUM_SAMPLES個のデータの平均を計算
    float sum = 0.0;
    for (int j = 0; j < NUM_SAMPLES; j++) {
      sum += prevAcc[j][i];
    }
    ac[i] = sum / NUM_SAMPLES;
  }
}

void setup() {
  M5.begin();
  
  Serial.begin(115200);
  Wire.begin();

  IMU.initMPU9250();

  M5.Lcd.setTextSize(2);
  M5.Lcd.setCursor(0,0);
  M5.Lcd.println("MPU9250 / AK8963");
}

void loop() {
  float ac[3];
  readAcc(ac);

  // 直近の加速度データを保存
  for (int i = 0; i < 3; i++) {
    prevAcc[sampleIndex][i] = ac[i];
  }
  
  // 移動平均フィルターを適用
  applyMovingAverage(ac);

  Serial.printf("%5.2f,%5.2f,%5.2f", ac[0], ac[1], ac[2]);
  Serial.println();
  
  // インデックスを更新して、サンプルデータを循環させる
  sampleIndex = (sampleIndex + 1) % NUM_SAMPLES;

  delay(10);
}
