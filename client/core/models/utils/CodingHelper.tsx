// Encoding UTF8 ⇢ base64
function byte64EncodeUnicode(str: any) {
    return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g, function(p1) {
        return String.fromCharCode(parseInt(p1, 16))
    }))
}

// Decoding base64 ⇢ UTF8
function byte64DecodeUnicode(str: any) {
    return decodeURIComponent(Array.prototype.map.call(atob(str), function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
    }).join(''))
}

export default { byte64DecodeUnicode, byte64EncodeUnicode }