Shader "KHSShader/StencilMaskShader"
{
	Properties
	{
		//셰이더에 사용할 변수를 선언하고 material inspector에 노출
		[IntRange] _StencilId ("Stencil ID", range(0, 255)) = 0
	}

	SubShader
	{
		//태그를 통해서 쉐이더의 타입을 설정하게 됨
		//랜더 파이프라인을 설정해줌
		Tags
		{
			"RenderPipeline" = "UniversalPipeline"
			"RenderType" = "Opaque"
			//지오메트리 큐에 있는 데이터를 사용
			//이때 다른것보다 먼저 렌더링 되어야함
			"Queue" = "Geometry-1"
		}

		Pass
		{
			Blend Zero One //픽셀의 색상에 대하여 이 셰이더가 출력한 색상의 0%와 이 픽셀에서 이미 랜더링된 100 을 취함
			
			//이 Shader는 프레임 버퍼에 영향을 주지 않아 Zbuffer 사용 x
			ZWrite Off 

			//다른 버퍼에 씌어 셰이더가 망가지는 것을 방지
			//마스킹레이어 자체는 렌더링되지 않도록 함
			ColorMask 0

			//앞면만 사용
			Cull front

			Stencil
			{
				Ref [_StencilId]
				
				//ref값을 현제 버퍼와 비교

				//스탠실 버퍼에 무엇이 있던지 패스
				Comp Always
				//스텐실 마스크와 depth test를 전부 통과해야 교체통과를 실행
				//버퍼에 ref값을 적용
				Pass Replace
				Fail Keep
			}
		}
	}
}
