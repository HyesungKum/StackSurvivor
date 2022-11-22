Shader "KHSShader/StencilMaskShader"
{
	Properties
	{
		//���̴��� ����� ������ �����ϰ� material inspector�� ����
		[IntRange] _StencilId ("Stencil ID", range(0, 255)) = 0
	}

	SubShader
	{
		//�±׸� ���ؼ� ���̴��� Ÿ���� �����ϰ� ��
		//���� ������������ ��������
		Tags
		{
			"RenderPipeline" = "UniversalPipeline"
			"RenderType" = "Opaque"
			//������Ʈ�� ť�� �ִ� �����͸� ���
			//�̶� �ٸ��ͺ��� ���� ������ �Ǿ����
			"Queue" = "Geometry-1"
		}

		Pass
		{
			Blend Zero One //�ȼ��� ���� ���Ͽ� �� ���̴��� ����� ������ 0%�� �� �ȼ����� �̹� �������� 100 �� ����
			
			//�� Shader�� ������ ���ۿ� ������ ���� �ʾ� Zbuffer ��� x
			ZWrite Off 

			//�ٸ� ���ۿ� ���� ���̴��� �������� ���� ����
			//����ŷ���̾� ��ü�� ���������� �ʵ��� ��
			ColorMask 0

			//�ո鸸 ���
			Cull front

			Stencil
			{
				Ref [_StencilId]
				
				//ref���� ���� ���ۿ� ��

				//���Ľ� ���ۿ� ������ �ִ��� �н�
				Comp Always
				//���ٽ� ����ũ�� depth test�� ���� ����ؾ� ��ü����� ����
				//���ۿ� ref���� ����
				Pass Replace
				Fail Keep
			}
		}
	}
}
