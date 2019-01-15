//
//  Speech.swift
//  SwiftPlugin
//
//  Created by Yutaka Miyazaki on 2018/11/28.
//  Copyright © 2018 Yutaka Miyazaki. All rights reserved.
//

import Foundation
import Speech

public class SpeechAPI : NSObject, SFSpeechRecognizerDelegate {
    static let sharedInstance: SpeechRecognizer = SpeechRecognizer()

    private var _unitySendMessageGameObjectName: String = "SpeechRecognizer"
    var unitySendMessageGameObjectName: String {
        get {
            return _unitySendMessageGameObjectName
        }
        set {
            _unitySendMessageGameObjectName = newValue
        }
    }

    // Englishにしたいならen-US
    private let speechRecognizer = SFSpeechRecognizer(locale: Locale(identifier: "ja-JP"))!
    private var recognitionRequest = SFSpeechAudioBufferRecognitionRequest()
    private var recognitionTask: SFSpeechRecognitionTask?
    private let audioEngine = AVAudioEngine()
    
    private override init() {
        super.init()
        speechRecognizer.delegate = self
    }

    // iosでマイクを使いたいユーザーに利用許可を尋ねるやーつ
    func requestRecognizerAuthorization() {
        SFSpeechRecognizer.requestAuthorization { authStatus in
            OperationQueue.main.addOperation {
                switch authStatus {
                // 許可された場合
                case .authorized:
                    self.unitySendMessage("OnAuthorized")
                    break
                
                // 拒否された場合
                case .denied:
                    // User denied access to speech recognition
                    self.unitySendMessage("OnUnauthorized")
                
                // そもそも使えない場合？
                case .restricted:
                    // Speech recognition restricted on this device
                    self.unitySendMessage("OnUnauthorized")
                
                // 許可されずなままな場合
                case .notDetermined:
                    // Speech recognition not yet authorized
                    self.unitySendMessage("OnUnauthorized")
                }
            }
        }
    }

    // Unityで使いたい開始する時のやーつ
    func startRecord() -> Bool {
        if audioEngine.isRunning {
            return false
        }
        // privateメソッド参照
        try! startRecording()
        print("始まるよ！")
        return true
    }

    // Unityで使いたい止める時のやーつ
    // 音声認識を停止するよ
    func stopRecord() -> Bool {
        if !audioEngine.isRunning {
            return false
        }
        print("止まるよ！")
        audioEngine.stop()
        recognitionTask?.finish()
        recognitionRequest.endAudio()
        return true
    }
    

    // private メソッド

    //Record ---> 録音の開始
    private func startRecording() throws {
      print("動き始めた！")
        refreshTask()

        let audioSession = AVAudioSession.sharedInstance()
        try audioSession.setCategory(AVAudioSessionCategoryRecord)
        try audioSession.setMode(AVAudioSessionModeMeasurement)
        try audioSession.setActive(true, with: .notifyOthersOnDeactivation)
        
        // recognitionRequest = SFSpeechAudioBufferRecognitionRequest()
        
        guard let inputNode = audioEngine.inputNode else { fatalError("Audio engine has no input node") }
        // guard let recognitionRequest = recognitionRequest else { fatalError("Unable to created a SFSpeechAudioBufferRecognitionRequest object") }
        
        recognitionRequest.shouldReportPartialResults = true
        
        // A recognition task represents a speech recognition session.
        // We keep a reference to the task so that it can be cancelled.
        recognitionTask = speechRecognizer.recognitionTask(with: recognitionRequest) { result, error in
            // ここでログの確認ができるよ
            print(result?.bestTranscription.formattedString ?? "")
            var isFinal = false
            
            if let result = result {
              // messageの内容で音声認識の内容が確認できるはず
              self.unitySendMessage("OnRecognized", message: bestTranscription.formattedString ?? "")
              isFinal = result.isFinal
            }
            
            if error != nil || isFinal {
                // エラーが起きたら終了
                self.audioEngine.stop()
                inputNode.removeTap(onBus: 0)
                
                // self.recognitionRequest = nil
                self.recognitionTask = nil
                self.unitySendMessage("OnError", message: error.debugDescription)
            }
        }
        
        let recordingFormat = inputNode.outputFormat(forBus: 0)
        inputNode.installTap(onBus: 0, bufferSize: 1024, format: recordingFormat) { (buffer: AVAudioPCMBuffer, when: AVAudioTime) in
            self.recognitionRequest.append(buffer)
        }

        try startAudioEngine()
    }

    // 音声認識をRefresh!!
    private func refreshTask() {
        if let recognitionTask = recognitionTask {
            recognitionTask.cancel()
            self.recognitionTask = nil
        }
    }
    
    // 音声認識を開始するよ
    private func startAudioEngine() throws {
        audioEngine.prepare()
        print("動いているよ！")
        try audioEngine.start()
    }

    func unitySendMessage(_ methodName: String, message: String = "") {
        UnitySendMessage(self.unitySendMessageGameObjectName, methodName, message)
    }
}

// extension SpeechAPI: SFSpeechRecognizerDelegate {
//     public func speechAPI(_ speechRecognizer: SFSpeechRecognizer, availabilityDidChange available: Bool) {
//         if (available) {
//             unitySendMessage("OnAvailable")
//         } else {
//             unitySendMessage("OnUnavailable")
//         }
//     }
// }
