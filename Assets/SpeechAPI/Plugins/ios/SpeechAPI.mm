//
//  Speech.swift
//  SwiftPlugin
//
//  Created by Yutaka Miyazaki on 2018/11/28.
//  Copyright © 2018 Yutaka Miyazaki. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <Speech/Speech.h>
#import "unityswift-Swift.h"

#pragma mark - C interface

// Unityで使えるようにObjective-Cでexterrrrrrn!!

extern "C" {
    void _sr_requestRecognizerAuthorization() {
        [[SpeechAPI sharedInstance] requestRecognizerAuthorization];
    }

    BOOL _sr_startRecord() {
        return [[SpeechAPI sharedInstance] startRecord];
    }
    
    BOOL _sr_stopRecord() {
        return [[SpeechAPI sharedInstance] stopRecord];
    }

    void _sr_setCallbackGameObjectName(const char *callbackGameObjectName) {
        [[SpeechAPI sharedInstance] setUnitySendMessageGameObjectName:[NSString stringWithUTF8String:callbackGameObjectName]];
    }
}
