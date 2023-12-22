import React from "react";

export default class StateService {

    generateRandomState(length = 32) {
        const bytes = new Uint8Array(length);
        const result = [];
        const charset = '0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._~';
      
        const crypto = window.crypto || window.msCrypto;
        const random = crypto.getRandomValues(bytes);
        for (let i = 0; i < random.length; i += 1) result.push(charset[random[i] % charset.length]);
      
        return result.join('');
      }
      
}