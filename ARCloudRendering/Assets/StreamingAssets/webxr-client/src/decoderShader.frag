uniform sampler2D u_encodedImage;
uniform int width;
uniform int height;

void main(){
    vec2 xy = gl_FragCoord.xy;
    vec3 rgb = texture2D(u_encodedImage, xy).rgb;
    float a = texture2D(u_encodedImage, xy + vec2(width, 0.0)).a;

    gl_FragColor = vec4(rgb, a);
}