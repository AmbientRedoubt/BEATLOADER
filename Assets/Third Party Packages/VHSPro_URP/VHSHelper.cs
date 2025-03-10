﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace VladStorm {

	public static class VHSHelper {

		public static List<PalettePreset> palettes;	
		public static string[] paletteNames;

		public static List<ResPreset> resPresets;	
		public static string[] presetNames;

		public static Dictionary<string, int> spIds; //shader property ids		
		public static Dictionary<string, LocalKeyword> shaderKeywords; //shader keywords

		public static bool isInitialized = false;

		public static 	string[] colorModes = {
			"Grayscale", 			//0								
			"RGB",					//1
			"YIQ (NTSC)"			//2
			// "YIQ (YI-Synced)", 	//3
		};

		public static 	string[] ditherModes = {
			"No Dithering",
			"Horizontal Lines",
			"Vertical Lines",
			"2x2 Ordered",
			"2x2 Ordered 2",
			"Uniform noise",
			"Triangle noise"
		};

		public static void Init(){

			//Note: when we press stop after play mode -> unity erases textures. so we need to recreate them 
			if(palettes!=null && palettes.Count>1){
				PalettePreset pp = palettes[0];
				Texture2D tex = pp.texSortedPre;
				if(tex==null) isInitialized = false;
				// Debug.Log("palette check" + tex);
			}

			if(isInitialized) return;
			InitPalettes();
			InitResolutions();
			InitShaderPropertyIds();

			isInitialized = true;
		}


		public static void InitShaderPropertyIds(){

	      //shader properties ids
	      spIds = new Dictionary<string, int>();  
	      Action<string> spIds_Add = (key) => { 
	      	spIds.Add(key, Shader.PropertyToID(key)); 
	      };

			spIds_Add("_time");
			spIds_Add("_ResOg");
			spIds_Add("_Res");
			spIds_Add("_ResN");

			//tape pass
			spIds_Add("filmGrainAmount");
			spIds_Add("tapeNoiseAmt");
			spIds_Add("tapeNoiseAlpha");
			spIds_Add("tapeNoiseSpeed");
			spIds_Add("lineNoiseAmount");
			spIds_Add("lineNoiseSpeed");

			spIds_Add("_colorMode");
			spIds_Add("_colorSyncedOn");
			spIds_Add("bitsR");
			spIds_Add("bitsG");
			spIds_Add("bitsB");
			spIds_Add("bitsSynced");
			spIds_Add("bitsGray");
			spIds_Add("grayscaleColor");

			spIds_Add("_ditherMode");
			spIds_Add("ditherAmount");
			spIds_Add("signalAdjustY");
			spIds_Add("signalAdjustI");
			spIds_Add("signalAdjustQ");
			spIds_Add("signalShiftY");
			spIds_Add("signalShiftI");
			spIds_Add("signalShiftQ");

			spIds_Add("signalMin");
			spIds_Add("signalMax");
			spIds_Add("signalNorm");



			spIds_Add("jitterHAmount");
			spIds_Add("jitterVAmount");
			spIds_Add("jitterVSpeed");
			spIds_Add("linesFloatSpeed");
			spIds_Add("twitchHFreq");
			spIds_Add("twitchVFreq");
			spIds_Add("scanLineWidth");
			spIds_Add("signalNoisePower");
			spIds_Add("signalNoiseAmount");

			spIds_Add("bleedAmount");
			spIds_Add("bleedMode");
			
			spIds_Add("_ResPalette");
			spIds_Add("paletteDelta");

			spIds_Add("feedbackThresh");
			spIds_Add("feedbackAmount");
			spIds_Add("feedbackFade");
			spIds_Add("feedbackColor");

			spIds_Add("feedbackOn");
			spIds_Add("feedbackDebugOn");


			spIds_Add("_TexTape");
			spIds_Add("_TexPalette");
			spIds_Add("_TexLast");
			spIds_Add("_TexFeedbackLast");


	      //TODO
	      //shader keywords 
	      // shaderKeywords = new Dictionary<string, LocalKeyword>();
	      // shaderKeywords.Add("EXAMPLE_FEATURE_ON", new LocalKeyword(mat1.shader, "EXAMPLE_FEATURE_ON"));


		}

		public static void InitPalettes(){

			palettes = new List<PalettePreset>();
			AddPalette("CGA > Full Palette", 								"pal_cga_full");
			AddPalette("CGA > Palette 1 High Intensity", 				"pal_cga_pal1_hi");
			AddPalette("CGA > Palette 1 Low Intensity", 					"pal_cga_pal1_low");
			AddPalette("CGA > Palette 2 High Intensity", 				"pal_cga_pal2_hi");
			AddPalette("CGA > Palette 2 Low Intensity Brown", 			"pal_cga_pal2_low");
			AddPalette("CGA > Palette 2 Low Intensity Dark Yellow", 	"pal_cga_pal2_low_2");
			AddPalette("ATR > NTSC", 											"pal_atr_ntsc");
			AddPalette("ATR > PAL", 											"pal_atr_pal");
			AddPalette("ATR > SCM", 											"pal_atr_scm");
			AddPalette("DOS > DN3D", 											"pal_dn");
			AddPalette("DOS > DM1", 											"pal_dm");
			AddPalette("DOS > QK1", 											"pal_qk");
			AddPalette("NTD > GB", 												"pal_gb");
			AddPalette("NTD > NES", 											"pal_nes");
			AddPalette("APL > Palette Hi", 									"pal_apl_hi");
			AddPalette("APL > Palette Low", 									"pal_apl_low");
			AddPalette("TTX", 													"pal_ttx");

			// AddPalette("Custom", 												"");
			// palettes[palettes.Count-1].isCustom = true;


			paletteNames = new string[palettes.Count];
			for(int i=0;i<palettes.Count;i++){
				paletteNames[i] = palettes[i].name;
			}

		}


		static void AddPalette(string name_, string filename_){ //, bool isCustom_=false
			PalettePreset p = new PalettePreset(name_, filename_); //, isCustom_
			palettes.Add(p);
		}	

		
		static public List<PalettePreset> GetPalettes(){
			if(!isInitialized) Init();
			return palettes;
		}

		static public string[] GetPaletteNames(){
			if(!isInitialized) Init();
			return paletteNames;
		}

		
		public static void InitResolutions(){

			resPresets = new List<ResPreset>();

			AddResPreset("Fullscreen", 								4,4);
			resPresets[resPresets.Count-1].isFirst = true;

			AddResPreset("PAL 240 Lines", 							320,240);
			AddResPreset("NTSC 480 Lines", 							640,480);

			AddResPreset("TTX'76L 78×69", 							78,69);

			AddResPreset("APL'77 Lo-Res 40×48", 					40, 48);
			AddResPreset("APL'77 Hi-Res 280×192", 					280, 192);

			AddResPreset("ATR'77 160x192 NTSC", 					160, 192);

			AddResPreset("CGA'81 320x200 ", 						320, 200);

			AddResPreset("CGA'81 160x100", 						160, 100);
			AddResPreset("CGA'81 640×200", 						640, 200);
			
			AddResPreset("ZXS'82 256×192", 						256, 192);

			AddResPreset("C64'82 160×200",						160, 200);
			AddResPreset("C64'82 Hi-Res 320×200", 				320, 200);

			AddResPreset("ACPC'84 160×200", 					160, 200);
			AddResPreset("ACPC'84 320×200", 					320, 200);
			AddResPreset("ACPC'84 640×200", 					640, 200);
			
			AddResPreset("Custom", 								4,4);
			resPresets[resPresets.Count-1].isCustom = true;
		

			presetNames = new string[resPresets.Count];
			for(int i=0;i<resPresets.Count;i++){
				presetNames[i] = resPresets[i].name;			
			}

			// isresPresetsInitialized = true;

		}


		static void AddResPreset(string name_, int screenWidth_, int screenHeight_ ){

			ResPreset p = new ResPreset(
										name_, 
										screenWidth_,
										screenHeight_
									);

			resPresets.Add(p);

		}


		static public List<ResPreset> GetResPresets(){
			if(!isInitialized) Init();
			return resPresets;
		}

		static public string[] GetResPresetNames(){
			if(!isInitialized) Init();
			return presetNames;
		}	

	    static public Vector2 Vector2Clamp(Vector2 val, Vector2 min, Vector2 max){
	        return new Vector2(
	            Mathf.Clamp(val.x, min.x, max.x),
	            Mathf.Clamp(val.y, min.y, max.y)
	        );
	    }
			
	}
}

