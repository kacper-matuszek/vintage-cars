export default function generateBasicUrl() {
    const apiSettings = require("../api-settings.json");
    
    let result = `${apiSettings.protocol}://${apiSettings.domain}:${apiSettings.port}/`;

    if(apiSettings.mainEndpoint)
        result = result.concat(apiSettings.mainEndpoint);

    return result;
} 