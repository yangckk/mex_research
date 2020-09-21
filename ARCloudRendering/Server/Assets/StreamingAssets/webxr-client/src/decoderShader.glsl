#ifdef GL_ES
precision mediump float;
#endif

#define WIDTH 237

uniform sampler2D u_tex0;

void main(){
    vec2 xy = gl_FragCoord.xy;
    //vec3 rgb = texture2D(u_tex0, xy).rgb;
    //float a = texture2D(u_tex0, xy + vec2(WIDTH, 0.0)).a;

    //gl_FragColor = vec4(rgb, a);
    gl_FragColor = texture2D(u_tex0, xy).rgba;
}